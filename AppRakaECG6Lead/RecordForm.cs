using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ScottPlot.Plottables;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AppRakaECG6Lead
{
    public partial class RecordForm : Form
    {
        long currentRecordId;

        // Variabel Penyimpan Data JSON
        List<double> rec1, rec2, rec3, rec4, rec5, rec6, recBpm;

        // Variabel Playback
        Timer timerPlay;
        DataStreamer stream1, stream2, stream3, stream4, stream5, stream6;

        private void dtpBorn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void RecordForm_Load(object sender, EventArgs e)
        {

        }

        // Variable Gap
        int gapCounter = 0;

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void panelNavbar_Paint(object sender, PaintEventArgs e)
        {

        }

        int gapDuration = 250; // 250 titik = 1 detik jeda layar kosong

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (timerPlay.Enabled)
            {
                // Jika sedang jalan, hentikan pemutaran
                timerPlay.Stop();
                btnPause.Text = "RESUME";
                btnPause.BackColor = System.Drawing.Color.Orange;
            }
            else
            {
                // Jika sedang berhenti, lanjutkan pemutaran
                timerPlay.Start();
                btnPause.Text = "PAUSE";
                btnPause.BackColor = System.Drawing.Color.Navy;
            }
        }

        // --- 6. EVENT: TOMBOL UPDATE IDENTITAS ---
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();
                    // Query untuk mengupdate data identitas pasien berdasarkan ID Rekaman yang sedang dibuka
                    string query = "UPDATE records SET pasien_id = @pid, pasien_name = @pname, pasien_gender = @pgender, pasien_born = @pborn WHERE record_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentRecordId);
                        cmd.Parameters.AddWithValue("@pid", txtPasienId.Text.Trim());
                        cmd.Parameters.AddWithValue("@pname", txtPasienName.Text.Trim());
                        cmd.Parameters.AddWithValue("@pgender", cmbGender.Text == "Laki-Laki" ? "L" : "P");

                        // Konversi format tanggal agar sesuai dengan format MySQL (YYYY-MM-DD)
                        cmd.Parameters.AddWithValue("@pborn", dtpBorn.Value.ToString("yyyy-MM-dd"));

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Identitas Pasien berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal update data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int playIndex = 0;

        public RecordForm(long recordId)
        {
            InitializeComponent();
            currentRecordId = recordId;

            SetupGrafik();
            MuatDataDariDB();
        }

        // --- 1. SETUP GRAFIK & TEMA EKG ---
        private void SetupGrafik()
        {
            stream1 = formsPlot1.Plot.Add.DataStreamer(250);
            stream2 = formsPlot2.Plot.Add.DataStreamer(250);
            stream3 = formsPlot3.Plot.Add.DataStreamer(250);
            stream4 = formsPlot4.Plot.Add.DataStreamer(250);
            stream5 = formsPlot5.Plot.Add.DataStreamer(250);
            stream6 = formsPlot6.Plot.Add.DataStreamer(250);

            stream1.ViewScrollLeft(); stream2.ViewScrollLeft(); stream3.ViewScrollLeft();
            stream4.ViewScrollLeft(); stream5.ViewScrollLeft(); stream6.ViewScrollLeft();

            FormatGrafikECG(formsPlot1, stream1, "Lead I");
            FormatGrafikECG(formsPlot2, stream2, "Lead II");
            FormatGrafikECG(formsPlot3, stream3, "Lead III");
            FormatGrafikECG(formsPlot4, stream4, "aVR");
            FormatGrafikECG(formsPlot5, stream5, "aVL");
            FormatGrafikECG(formsPlot6, stream6, "aVF");

            timerPlay = new Timer();
            timerPlay.Interval = 33;
            timerPlay.Tick += TimerPlay_Tick;
        }

        private void FormatGrafikECG(ScottPlot.WinForms.FormsPlot fp, DataStreamer stream, string title)
        {
            fp.Plot.Title(title);
            stream.Color = ScottPlot.Colors.Black;
            stream.LineWidth = 1.5f;

            fp.Plot.FigureBackground.Color = ScottPlot.Colors.White;
            fp.Plot.DataBackground.Color = ScottPlot.Colors.White;
            fp.Plot.Grid.MajorLineColor = ScottPlot.Colors.RoyalBlue;
            fp.Plot.Grid.MajorLineWidth = 1;

            fp.Plot.Axes.Left.TickLabelStyle.IsVisible = false;
            fp.Plot.Axes.Left.MajorTickStyle.Length = 0;
            fp.Plot.Axes.Left.MinorTickStyle.Length = 0;
            fp.Plot.Axes.Bottom.TickLabelStyle.IsVisible = false;
            fp.Plot.Axes.Bottom.MajorTickStyle.Length = 0;
            fp.Plot.Axes.Bottom.MinorTickStyle.Length = 0;

            fp.Plot.Axes.Right.IsVisible = false;
            fp.Plot.Axes.Top.IsVisible = false;

            fp.Plot.Layout.Fixed(new ScottPlot.PixelPadding(0, 0, 0, 30));
            // 1. Seragamkan margin atas-bawah agar grafik tidak gepeng
            fp.Plot.Axes.Margins(left: 0, right: 0, bottom: 0.2, top: 0.2);

            // 2. Set ukuran tengah absolut
            fp.Plot.Axes.SetLimitsY(150, 900);

            // 3. MATIKAN INTERAKSI MOUSE (ScottPlot 5)
            // Cara utama: Mematikan respon terhadap scroll, klik, dan drag
            fp.UserInputProcessor.Disable();

            // Cara ganda (opsional tapi sangat disarankan): 
            // Memasang gembok langsung pada Sumbu Y agar tidak bisa bergeser sama sekali
            fp.Plot.Axes.Rules.Clear();
            fp.Plot.Axes.Rules.Add(new ScottPlot.AxisRules.LockedVertical(fp.Plot.Axes.Left, 150, 900));
        }

        // --- 2. AMBIL DATA DARI DATABASE (SUPER CEPAT & RINGAN) ---
        private void MuatDataDariDB()
        {
            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();

                    // A. Ambil Identitas Pasien SEKALIGUS Status Diagnosis dari DB
                    string queryRecord = @"SELECT r.*, u.fullname 
                                           FROM records r 
                                           LEFT JOIN users u ON r.user_id = u.user_id 
                                           WHERE r.record_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(queryRecord, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentRecordId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPasienId.Text = reader["pasien_id"].ToString();
                                txtPasienName.Text = reader["pasien_name"].ToString();
                                cmbGender.Text = reader["pasien_gender"].ToString() == "L" ? "Laki-Laki" : "Perempuan";
                                dtpBorn.Value = Convert.ToDateTime(reader["pasien_born"]);

                                lblWaktu.Text = Convert.ToDateTime(reader["record_at"]).ToString("dd/MM/yyyy HH:mm:ss");
                                lblNamaUser.Text = reader["fullname"].ToString();

                                // =========================================================
                                // LANGSUNG TAMPILKAN STATUS DIAGNOSIS DARI DATABASE!
                                // =========================================================
                                string hasilDiagnosis = reader["status_diagnosis"].ToString();
                                lblStatus.Text = hasilDiagnosis;

                                // Atur pewarnaan latar belakang label sesuai bahayanya
                                if (hasilDiagnosis == "SINUS NORMAL")
                                {
                                    lblStatus.BackColor = System.Drawing.Color.LightGreen;
                                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                                }
                                else if (hasilDiagnosis == "SINUS ARRHYTHMIA" || hasilDiagnosis == "SINUS BRADYCARDIA" || hasilDiagnosis == "SINUS TACHYCARDIA")
                                {
                                    lblStatus.BackColor = System.Drawing.Color.Khaki;
                                    lblStatus.ForeColor = System.Drawing.Color.DarkGoldenrod;
                                }
                                else // ATRIAL FIBRILLATION & VENTRICULAR FIBRILLATION
                                {
                                    lblStatus.BackColor = System.Drawing.Color.Crimson;
                                    lblStatus.ForeColor = System.Drawing.Color.White;
                                }
                            }
                        }
                    }

                    // B. Ambil Data JSON (Untuk Playback Grafik)
                    string queryData = "SELECT * FROM records_datas WHERE record_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(queryData, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentRecordId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rec1 = JsonConvert.DeserializeObject<List<double>>(reader["data_1"].ToString());
                                rec2 = JsonConvert.DeserializeObject<List<double>>(reader["data_2"].ToString());
                                rec3 = JsonConvert.DeserializeObject<List<double>>(reader["data_3"].ToString());
                                rec4 = JsonConvert.DeserializeObject<List<double>>(reader["data_4"].ToString());
                                rec5 = JsonConvert.DeserializeObject<List<double>>(reader["data_5"].ToString());
                                rec6 = JsonConvert.DeserializeObject<List<double>>(reader["data_6"].ToString());
                                recBpm = JsonConvert.DeserializeObject<List<double>>(reader["data_bpm"].ToString());
                            }
                        }
                    }
                }

                // Langsung nyalakan timer animasi grafik (tanpa perlu proses analisa lagi!)
                if (rec1 != null && rec1.Count > 0)
                {
                    timerPlay.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================================
        // 2. ANIMASI PLAYBACK GRAFIK DENGAN ANTI-CRASH SHIELD (100% AMAN DARI CLOSE)
        // =========================================================================
        private bool isRendering = false; // Gerbang pengaman bentrokan memori grafis

        private void TimerPlay_Tick(object sender, EventArgs e)
        {
            if (isRendering || rec1 == null || rec1.Count == 0) return;

            try
            {
                isRendering = true;
                int dataPerTick = 12; // 12 data x 20 FPS (50ms) = 240 Hz ~ 250 Hz

                for (int i = 0; i < dataPerTick; i++)
                {
                    if (playIndex >= rec1.Count)
                    {
                        gapCounter++;

                        stream1.AddRange(new double[] { double.NaN });
                        stream2.AddRange(new double[] { double.NaN });
                        stream3.AddRange(new double[] { double.NaN });
                        stream4.AddRange(new double[] { double.NaN });
                        stream5.AddRange(new double[] { double.NaN });
                        stream6.AddRange(new double[] { double.NaN });

                        lblBPM.Text = "-";

                        if (gapCounter >= gapDuration)
                        {
                            playIndex = 0;
                            gapCounter = 0;
                        }
                    }
                    else
                    {
                        // INDEX SHIELD: Pengecekan batas array mencegah aplikasi langsung tutup sendiri!
                        if (playIndex < rec1.Count) stream1.AddRange(new double[] { rec1[playIndex] });
                        if (rec2 != null && playIndex < rec2.Count) stream2.AddRange(new double[] { rec2[playIndex] });
                        if (rec3 != null && playIndex < rec3.Count) stream3.AddRange(new double[] { rec3[playIndex] });
                        if (rec4 != null && playIndex < rec4.Count) stream4.AddRange(new double[] { rec4[playIndex] });
                        if (rec5 != null && playIndex < rec5.Count) stream5.AddRange(new double[] { rec5[playIndex] });
                        if (rec6 != null && playIndex < rec6.Count) stream6.AddRange(new double[] { rec6[playIndex] });

                        if (recBpm != null && playIndex < recBpm.Count)
                        {
                            lblBPM.Text = $"{recBpm[playIndex]:F0}";
                        }

                        playIndex++;
                    }
                }

                formsPlot1.Refresh(); formsPlot2.Refresh(); formsPlot3.Refresh();
                formsPlot4.Refresh(); formsPlot5.Refresh(); formsPlot6.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Glitch Render Terlewati: " + ex.Message);
            }
            finally
            {
                isRendering = false;
            }
        }

        // =========================================================================
        // 3. PENCEGAHAN FATAL EXCEPTION SAAT FORM DITUTUP (SILENT CRASH KILLER)
        // =========================================================================
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 1. KUNCI GERBANG RENDER
            // Mencegah Timer atau ScottPlot menggambar frame baru saat form sedang ditutup
            isRendering = true;

            // 2. MATIKAN TIMER PLAYBACK TOTAL
            if (timerPlay != null)
            {
                timerPlay.Stop();
                timerPlay.Tick -= TimerPlay_Tick; // Putus sambungan event
                timerPlay.Dispose();
                timerPlay = null;
            }

            // 3. KOSONGKAN DATA STREAMER AGAR MEMORI C++ SKIASHARP DILEPASKAN
            try
            {
                if (formsPlot1 != null) formsPlot1.Plot.Clear();
                if (formsPlot2 != null) formsPlot2.Plot.Clear();
                if (formsPlot3 != null) formsPlot3.Plot.Clear();
                if (formsPlot4 != null) formsPlot4.Plot.Clear();
                if (formsPlot5 != null) formsPlot5.Plot.Clear();
                if (formsPlot6 != null) formsPlot6.Plot.Clear();
            }
            catch { /* Abaikan jika sudah ter-dispose */ }

            // 4. BERI JEDA NAFAS 50 MILIDETIK UNTUK PROSESOR GRAFIS
            // Ini adalah kunci utama pencegah 0xc0000409! Memberi waktu bagi C++ SkiaSharp 
            // menyelesaikan antrean kerja sebelum RAM dihancurkan oleh Windows.
            System.Threading.Thread.Sleep(50);

            // 5. LANJUTKAN PENUTUPAN FORM
            base.OnFormClosing(e);
        }


        // --- 4. EVENT: TOMBOL KEMBALI ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Matikan timer sebelum menutup form agar tidak membebani memori
            timerPlay.Stop();
            this.Close();
        }

        // --- 5. EVENT: TOMBOL CETAK PDF ---
        private void btnCetak_Click(object sender, EventArgs e)
        {
            bool wasPlaying = timerPlay.Enabled;
            if (wasPlaying) timerPlay.Stop();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Document|*.pdf";
            sfd.FileName = $"EKG_Report_{txtPasienName.Text}_{DateTime.Now:ddMMyyyy}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1.Setup Dokumen PDF
                    Document doc = new Document(PageSize.A4, 30, 30, 30, 30);
                    PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                    doc.Open();

                    // 2. Judul & Identitas
                    iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                    Paragraph title = new Paragraph("LAPORAN PEMERIKSAAN ELEKTROKARDIOGRAM", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);
                    doc.Add(new Chunk("\n"));

                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    table.AddCell(new Phrase("No. RM: " + txtPasienId.Text));
                    table.AddCell(new Phrase("Waktu Rekam: " + lblWaktu.Text));
                    table.AddCell(new Phrase("Nama Pasien: " + txtPasienName.Text));
                    table.AddCell(new Phrase("Petugas: " + lblNamaUser.Text));
                    table.AddCell(new Phrase("Jenis Kelamin: " + cmbGender.Text));
                    table.AddCell(new Phrase("BPM: " + lblBPM.Text));
                    table.AddCell(new Phrase("Tgl Lahir: " + dtpBorn.Value.ToString("dd-MM-yyyy")));
                    table.AddCell(new Phrase("Status: " + lblStatus.Text));

                    doc.Add(table);
                    doc.Add(new Chunk("\n"));
                    doc.Add(new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -1));
                    doc.Add(new Chunk("\n"));

                    // --- REVISI DOSEN: TAMBAHAN KETERANGAN RUMUS & JARAK STANDAR EKG ---
                    iTextSharp.text.Font calcFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.DARK_GRAY);
                    Paragraph kalibrasi = new Paragraph(
                        "Speed: 25 mm/sec    |    Chest/Limb: 10 mm/mV    |    50 Hz\n" +
                        "1 Kotak Kecil = 0.04 s (40 ms)    |    1 Kotak Besar = 0.20 s (200 ms)",
                        calcFont);
                    kalibrasi.Alignment = Element.ALIGN_CENTER;
                    doc.Add(kalibrasi);
                    doc.Add(new Chunk("\n"));
                    // -------------------------------------------------------------------

                    // 3. Loop Menggambar 6 Lead dengan Grid EKG Presisi
                    string[] namaLead = { "Lead I", "Lead II", "Lead III", "aVR", "aVL", "aVF" };
                    List<double>[] dataLead = { rec1, rec2, rec3, rec4, rec5, rec6 };

                    for (int i = 0; i < 6; i++)
                    {
                        if (dataLead[i] == null || dataLead[i].Count == 0) continue;

                        ScottPlot.Plot plt = new ScottPlot.Plot();

                        // --- 1. PENGATURAN KERTAS EKG (MANUAL GRID 200 Hz) ---
                        plt.FigureBackground.Color = ScottPlot.Colors.White;
                        plt.DataBackground.Color = ScottPlot.Colors.White;

                        // Matikan grid bawaan agar kita bisa menggambar grid presisi manual
                        plt.Grid.IsVisible = false;

                        // A. Gambar Garis Vertikal (Sumbu X - Waktu)
                        // Jarak 8 titik = 1 Kotak Kecil (40 ms), Jarak 40 titik = 1 Kotak Besar (200 ms)
                        for (int x = 0; x <= dataLead[i].Count; x += 8)
                        {
                            var vl = plt.Add.VerticalLine(x);
                            if (x % 40 == 0)
                            {
                                vl.Color = ScottPlot.Color.FromHtml("#FF7A7A"); // Pink Tua (Kotak Besar)
                                vl.LineWidth = 1.0f;
                            }
                            else
                            {
                                vl.Color = ScottPlot.Color.FromHtml("#FFC9C9"); // Pink Sedang (Kotak Kecil - Terlihat tapi tipis)
                                vl.LineWidth = 0.5f;
                            }
                        }

                        // B. Gambar Garis Horizontal (Sumbu Y - Amplitudo)
                        // Jarak 20 = 1 Kotak Kecil, Jarak 100 = 1 Kotak Besar
                        for (int y = 150; y <= 900; y += 20)
                        {
                            var hl = plt.Add.HorizontalLine(y);
                            if (y % 100 == 0)
                            {
                                hl.Color = ScottPlot.Color.FromHtml("#FF7A7A");
                                hl.LineWidth = 1.0f;
                            }
                            else
                            {
                                hl.Color = ScottPlot.Color.FromHtml("#FFC9C9");
                                hl.LineWidth = 0.5f;
                            }
                        }

                        // --- 2. PENGATURAN SIGNAL ---
                        var sig = plt.Add.Signal(dataLead[i].ToArray());
                        sig.Color = ScottPlot.Colors.Black;
                        sig.LineWidth = 1.2f;
                        plt.Title(namaLead[i]);

                        // Kunci limit koordinat agar tidak ada spasi sisa di pinggir gambar
                        plt.Axes.SetLimitsX(0, dataLead[i].Count);
                        plt.Axes.SetLimitsY(150, 900);

                        // Sembunyikan angka dan centang koordinat agar bersih
                        plt.Axes.Left.TickLabelStyle.IsVisible = false;
                        plt.Axes.Bottom.TickLabelStyle.IsVisible = false;
                        plt.Axes.Right.IsVisible = false;
                        plt.Axes.Top.IsVisible = false;
                        plt.Axes.Left.MajorTickStyle.Length = 0;
                        plt.Axes.Bottom.MajorTickStyle.Length = 0;

                        // --- 3. EKSPOR HD KE PDF ---
                        string tempImage = Path.Combine(Path.GetTempPath(), $"lead_print_{i}.png");
                        plt.SavePng(tempImage, 1200, 250);

                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(tempImage);
                        img.ScaleToFit(530f, 200f);
                        img.Alignment = Element.ALIGN_CENTER;
                        doc.Add(img);

                        File.Delete(tempImage);
                    }

                    doc.Close();
                    MessageBox.Show("PDF Berhasil Dibuat!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Cetak: " + ex.Message);
                }
            }

            if (wasPlaying) timerPlay.Start();
        }
    }
}