using System;
using System.Collections.Generic;
using System.Linq;

namespace AppRakaECG6Lead
{
    // =========================================================================
    // KELAS ANALISA EKG RULE-BASED (SISTEM PAKAR)
    // -------------------------------------------------------------------------
    // Deskripsi : Mendeteksi kelainan EKG menggunakan aturan medis If-Then 
    //             berdasarkan 3 Fitur Utama: Volatilitas (Getaran), 
    //             CV-RR (Ketidakteraturan), dan Hardware BPM dari ESP32.
    // Status    : Terkalibrasi dengan Simulator Phantom (V4)
    // =========================================================================
    public static class ECGRuleBase
    {
        // Fungsi Utama: Menerima 1250 data sinyal (5 detik) dan angka BPM dari alat
        public static string AnalisaSinyal(List<double> rawLeadII, double hardwareBpm)
        {
            // Validasi: Pastikan data yang masuk minimal 4-5 detik agar hitungan statistik valid
            if (rawLeadII == null || rawLeadII.Count < 1000)
                return "MENGUMPULKAN DATA...";

            try
            {
                // Membersihkan sinyal dari kemungkinan nilai error (NaN/Infinity) saat transmisi Bluetooth/Serial
                List<double> leadII = rawLeadII.Where(v => !double.IsNaN(v) && !double.IsInfinity(v)).ToList();
                double sampleRate = 250.0; // Frekuensi sampling alat (250 data per detik)

                // =========================================================
                // [TAHAP 1] EKSTRAKSI FITUR: VOLATILITAS (GETARAN SINYAL)
                // =========================================================
                // Tujuannya: Mengukur seberapa "liar" sinyal naik-turun.
                // Gelombang QRS normal itu tajam (volatilitas tinggi).
                // Gelombang VFib itu tumpul dan bergetar (volatilitas rendah-menengah).
                double totalVolatility = 0;
                for (int i = 1; i < leadII.Count; i++)
                {
                    totalVolatility += Math.Abs(leadII[i] - leadII[i - 1]);
                }
                double avgVolatility = totalVolatility / (leadII.Count - 1);

                // =========================================================
                // [TAHAP 2] EKSTRAKSI FITUR: DETEKSI PUNCAK GELOMBANG (R-PEAK)
                // =========================================================
                double avgSignal = leadII.Average();
                double maxVal = leadII.Max();

                // Membuat garis batas (Threshold). Puncak R harus berada di 50% bagian atas sinyal.
                double threshold = avgSignal + ((maxVal - avgSignal) * 0.50);

                // Jarak minimal antar detak jantung tidak mungkin lebih cepat dari 200ms (50 sampel)
                int minDistance = (int)(0.20 * sampleRate);

                List<int> rPeaks = new List<int>();
                for (int i = 2; i < leadII.Count - 2; i++)
                {
                    // Syarat Puncak: Titik saat ini harus lebih tinggi dari 2 titik sebelum & 2 titik sesudahnya
                    if (leadII[i] > threshold &&
                        leadII[i] > leadII[i - 1] && leadII[i] > leadII[i - 2] &&
                        leadII[i] > leadII[i + 1] && leadII[i] > leadII[i + 2])
                    {
                        // Tambahkan ke daftar jika jaraknya memenuhi batas aman (minDistance)
                        if (rPeaks.Count == 0 || (i - rPeaks.Last()) >= minDistance) rPeaks.Add(i);
                    }
                }

                // KONDISI PENGECUALIAN (KABEL LEPAS / ALAT MATI)
                // Jika tidak ada gelombang (Peak < 2) dan sinyal sangat datar (Volatilitas < 1.0)
                if (avgVolatility < 1.0 && rPeaks.Count < 2) return "SINYAL TIDAK TERBACA";

                // KONDISI PENGECUALIAN (VFIB EKSTREM)
                // Jika tidak ada QRS tajam (Peak < 2), TAPI sinyalnya bergetar tumpul di rentang Phantom VFib
                if (rPeaks.Count < 2)
                {
                    if (avgVolatility >= 1.5 && avgVolatility <= 6.0) return "VENTRICULAR FIBRILLATION";
                    return "MENGUMPULKAN DATA...";
                }

                // =========================================================
                // [TAHAP 3] EKSTRAKSI FITUR: CV-RR (KEDISIPLINAN IRAMA)
                // =========================================================
                // Tujuannya: Mencari tahu apakah jantung berdetak teratur atau acak (Arrhythmia)
                List<double> rrIntervals = new List<double>();
                for (int i = 1; i < rPeaks.Count; i++)
                {
                    rrIntervals.Add((rPeaks[i] - rPeaks[i - 1]) / sampleRate);
                }

                // Rumus Statistika Standar Deviasi untuk mencari persentase CV (Coefficient of Variation)
                double meanRR = rrIntervals.Average();
                double sumOfSquares = rrIntervals.Sum(rr => Math.Pow(rr - meanRR, 2));
                double stdDevRR = Math.Sqrt(sumOfSquares / rrIntervals.Count);
                double cvRR = stdDevRR / meanRR;

                // =========================================================
                // [TAHAP 4] POHON KEPUTUSAN / RULE-BASED (IF-THEN)
                // =========================================================

                // ATURAN 1: VENTRICULAR FIBRILLATION (Kondisi Kritis 1)
                // Syarat Medis & Kalibrasi: Getaran gelombang tumpul (Volatilitas 1.5 - 6.0) 
                //                           DAN iramanya sangat tidak beraturan (CV >= 15%)
                if (avgVolatility >= 1.5 && avgVolatility <= 6.0 && cvRR >= 0.15)
                {
                    return "VENTRICULAR FIBRILLATION";
                }

                // ATURAN 2: ATRIAL FIBRILLATION (Kondisi Kritis 2)
                // Syarat Medis: Gelombang QRS tajam terdeteksi (lolos dari Aturan 1), 
                //               TAPI jarak antar detaknya sangat acak (CV-RR >= 12%)
                else if (cvRR >= 0.12 && hardwareBpm < 120.0)
                {
                    return "ATRIAL FIBRILLATION";
                }

                // ATURAN 3: SINUS BRADYCARDIA
                // Syarat Medis: Irama teratur (CV < 12%), TAPI detak jantung sangat lambat (BPM <= 60).
                // Catatan   : Menggunakan angka BPM mutlak dari perangkat keras ESP32 agar akurat.
                else if (hardwareBpm <= 60.0)
                {
                    return "SINUS BRADYCARDIA";
                }

                // ATURAN 4: SINUS TACHYCARDIA
                // Syarat Medis: Irama teratur, TAPI detak jantung sangat cepat (BPM >= 100).
                else if (hardwareBpm >= 100.0)
                {
                    return "SINUS TACHYCARDIA";
                }

                // ATURAN 5: SINUS NORMAL
                // Syarat Medis: Jika semua aturan di atas tidak terpenuhi, maka pasien 
                //               memiliki irama teratur dengan BPM normal (61 - 99).
                else
                {
                    return "SINUS NORMAL";
                }
            }
            catch (Exception ex)
            {
                // Menangkap error jika tiba-tiba data Serial terputus di tengah jalan
                System.Diagnostics.Debug.WriteLine("Error Analisa Rule-Base: " + ex.Message);
                return "ANALISA ERROR";
            }
        }
    }
}