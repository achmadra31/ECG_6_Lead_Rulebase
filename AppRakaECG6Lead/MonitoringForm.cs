using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ScottPlot.Plottables;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace AppRakaECG6Lead
{
    public partial class MonitoringForm : Form
    {
        // Variabel Sistem
        SerialPort portArduino;
        Timer timerGambar;
        Timer timerWaktu;
        DataStreamer stream1, stream2, stream3, stream4, stream5, stream6;

        // --- GERBANG PENGAMAN BENTROKAN MEMORI GRAFIS (ANTI-CRASH) ---
        private bool isRendering = false;

        // Variabel Pengaturan
        string currentComPort = "";
        int recordTime = 10;

        // Variabel Perekaman (Auto ECG)
        bool isRecording = false;
        bool isDisconnected = false;
        DateTime startTime;
        List<double> rec1, rec2, rec3, rec4, rec5, rec6, recBpm;
        double currentBpm = 0;

        // --- VARIABEL KECERDASAN BUATAN (REAL-TIME ML) ---
        private List<double> bufferLead2Realtime = new List<double>(); // Penampung sinyal 4 detik
        private Timer timerAI; // Timer khusus AI agar tidak mengganggu rendering grafik
        private int maxBufferSize = 2600; // 2500 sampel = 10 detik pada 250 Hz
        private List<string> historyPrediksi = new List<string>();
        private List<double> historyBpmAI = new List<double>();


        public MonitoringForm()
        {
            InitializeComponent();
            SetupGrafik();
            MuatDataAwal();
            MuatPengaturanDanKoneksi();
        }

        private void FormatGrafikECG(FormsPlot fp, DataStreamer stream, string title)
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

        // --- 1. SETUP JAM, DATA USER & COMBOBOX ---
        private void MuatDataAwal()
        {
            lblNamaUser.Text = Session.Fullname;
            cmbGender.Items.Clear();
            cmbGender.Items.Add("Laki-Laki");
            cmbGender.Items.Add("Perempuan");
            cmbGender.SelectedIndex = 0;

            timerWaktu = new Timer();
            timerWaktu.Interval = 1000;
            timerWaktu.Tick += TimerWaktu_Tick;
            timerWaktu.Start();
        }

        // --- EVENT: TOMBOL DIRECTORY ---
        private void btnDirectory_Click(object sender, EventArgs e)
        {
            // PERBAIKAN: Matikan timer DAN putus penerimaan serial sementara agar RAM aman
            timerGambar.Stop();
            if (portArduino != null && portArduino.IsOpen) portArduino.DataReceived -= PortArduino_DataReceived;

            this.Hide();
            DirectoryForm directory = new DirectoryForm();
            directory.ShowDialog();
            this.Show();

            // Nyalakan kembali saat kembali ke monitoring
            if (portArduino != null && portArduino.IsOpen) portArduino.DataReceived += PortArduino_DataReceived;
            timerGambar.Start();
        }

        private void lblBPM_Click(object sender, EventArgs e) { }

        private void TimerWaktu_Tick(object sender, EventArgs e)
        {
            lblWaktu.Text = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss");
        }

        // --- 2. SETUP GRAFIK SCOTTPLOT ---
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

            timerGambar = new Timer();
            // PERBAIKAN: Ubah ke 50 ms (20 FPS) -> 40% lebih ringan untuk prosesor grafis!
            timerGambar.Interval = 50;
            timerGambar.Tick += TimerGambar_Tick;
            timerGambar.Start();

            // Setup Timer Khusus AI (Berdetak tiap 1 detik -> Sangat ringan & cepat!)
            timerAI = new Timer();
            timerAI.Interval = 1000; // 1 detik sekali
            timerAI.Tick += TimerAI_Tick;
            timerAI.Start();
        }

      
        private void MonitoringForm_Load(object sender, EventArgs e) {
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            timerGambar.Stop();
            if (portArduino != null && portArduino.IsOpen)
            {
                portArduino.DataReceived -= PortArduino_DataReceived;
                portArduino.Close();
            }

            SettingForm setting = new SettingForm();
            setting.ShowDialog();

            MuatPengaturanDanKoneksi();
            timerGambar.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            isDisconnected = !isDisconnected;

            if (isDisconnected)
            {
                // ==========================================
                // PROSES DISCONNECT (TOTAL STOP)
                // ==========================================
                try
                {
                    // 1. Batalkan rekam otomatis jika sedang berjalan agar data tidak rusak/putus
                    if (isRecording)
                    {
                        isRecording = false;
                        btnAutoEcg.Text = "AUTO ECG";
                        btnAutoEcg.BackColor = System.Drawing.Color.Navy;
                        btnAutoEcg.Enabled = true;
                    }

                    // 2. Hentikan timer animasi & AI agar CPU istirahat total
                    timerGambar.Stop();
                    timerAI.Stop();

                    // 3. Putus aliran data dari Port Serial secara aman
                    if (portArduino != null && portArduino.IsOpen)
                    {
                        portArduino.DataReceived -= PortArduino_DataReceived;
                        portArduino.Close();
                    }

                    // 4. Ubah tampilan tombol
                    btnPause.Text = "RESUME";
                    btnPause.BackColor = System.Drawing.Color.Orange;

                    if (lblStatus != null)
                    {
                        lblStatus.Text = "SYSTEM PAUSED (DISCONNECTED)";
                        lblStatus.BackColor = System.Drawing.Color.Gray;
                        lblStatus.ForeColor = System.Drawing.Color.White;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memutuskan koneksi: " + ex.Message);
                }
            }
            else
            {
                // ==========================================
                // PROSES RECONNECT (RESUME)
                // ==========================================
                try
                {
                    // 1. Bersihkan sisa gelombang lama di layar agar saat nyambung gelombangnya fresh (tidak loncat)
                    stream1.Clear(); stream2.Clear(); stream3.Clear();
                    stream4.Clear(); stream5.Clear(); stream6.Clear();
                    lock (bufferLead2Realtime) { bufferLead2Realtime.Clear(); }

                    // 2. Sambungkan kembali port serial
                    if (portArduino != null && !portArduino.IsOpen)
                    {
                        portArduino.DataReceived += PortArduino_DataReceived;
                        portArduino.Open();
                    }
                    else if (portArduino == null)
                    {
                        // Jaga-jaga jika objek port terhapus dari memori, buat ulang dari database
                        MuatPengaturanDanKoneksi();
                    }

                    // 3. Jalankan ulang timer grafik & AI
                    timerGambar.Start();
                    timerAI.Start();

                    // 4. Kembalikan tampilan tombol ke kondisi awal
                    btnPause.Text = "PAUSE";
                    btnPause.BackColor = System.Drawing.Color.Navy;

                    if (lblStatus != null)
                    {
                        lblStatus.Text = "MENGUMPULKAN DATA...";
                        lblStatus.BackColor = System.Drawing.Color.LightGray;
                        lblStatus.ForeColor = System.Drawing.Color.Black;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menyambung kembali. Pastikan kabel alat masih tertancap dengan baik!\n\nError: " + ex.Message, "Error Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Kembalikan ke mode Disconnected jika gagal connect
                    isDisconnected = true;
                    btnPause.Text = "RESUME";
                    btnPause.BackColor = System.Drawing.Color.Orange;
                }
            }
        }

        // --- 3. BACA DATABASE & KONEK SERIAL ---
        private void MuatPengaturanDanKoneksi()
        {
            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT setting_com, setting_recordtime FROM settings LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentComPort = reader["setting_com"].ToString();
                            recordTime = Convert.ToInt32(reader["setting_recordtime"]);
                        }
                    }
                }

                portArduino = new SerialPort();
                portArduino.PortName = currentComPort;
                portArduino.BaudRate = 115200;
                portArduino.DataReceived += PortArduino_DataReceived;
                portArduino.Open();
            }
            catch (Exception)
            {
                MessageBox.Show($"Gagal menyambung ke {currentComPort}.\nAlat mungkin belum tertancap atau Port salah.\n\nSilakan buka menu SETTING untuk mengatur ulang.",
                                "Koneksi Alat Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- 4. TERIMA DATA DARI ARDUINO ---
        private void PortArduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // PERBAIKAN: Cegah pengisian data jika form sedang di-dispose atau ditutup
            if (this.IsDisposed || !this.Visible) return;

            try
            {
                string line = portArduino.ReadLine().Trim();
                string[] data = line.Split(',');

                if (data.Length == 7)
                {
                    if (double.TryParse(data[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double bpm) &&
                        double.TryParse(data[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double s1) &&
                        double.TryParse(data[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double s2) &&
                        double.TryParse(data[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double s3) &&
                        double.TryParse(data[4], NumberStyles.Float, CultureInfo.InvariantCulture, out double s4) &&
                        double.TryParse(data[5], NumberStyles.Float, CultureInfo.InvariantCulture, out double s5) &&
                        double.TryParse(data[6], NumberStyles.Float, CultureInfo.InvariantCulture, out double s6))
                    {
                    

                        stream1.AddRange(new double[] { s1 });
                        stream2.AddRange(new double[] { s2 });
                        stream3.AddRange(new double[] { s3 }); 
                        stream4.AddRange(new double[] { s4 });
                        stream5.AddRange(new double[] { s5 });
                        stream6.AddRange(new double[] { s6 });

                        currentBpm = bpm;
                        

                        lock (bufferLead2Realtime)
                        {
                            bufferLead2Realtime.Add(s2);
                            if (bufferLead2Realtime.Count > maxBufferSize)
                            {
                                bufferLead2Realtime.RemoveAt(0); // Buang data paling lama
                            }
                        }

                        if (isRecording)
                        {
                            rec1.Add(s1);
                            rec2.Add(s2); rec3.Add(s3);
                            rec4.Add(s4); rec5.Add(s5); rec6.Add(s6);
                            recBpm.Add(bpm);

                            if ((DateTime.Now - startTime).TotalSeconds >= recordTime)
                            {
                                isRecording = false;
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    SimpanKeDatabase();
                                });
                            }
                        }
                    }
                }
            }
            catch { }
        }

        // --- 7. LOGIKA SIMPAN KE DATABASE (JSON) ---
        private void SimpanKeDatabase()
        {
            long newRecordId = 0;
            try
            {
                string json1 = JsonConvert.SerializeObject(rec1);
                string json2 = JsonConvert.SerializeObject(rec2);
                string json3 = JsonConvert.SerializeObject(rec3);
                string json4 = JsonConvert.SerializeObject(rec4);
                string json5 = JsonConvert.SerializeObject(rec5);
                string json6 = JsonConvert.SerializeObject(rec6);
                string jsonBpm = JsonConvert.SerializeObject(recBpm);

                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();

                    // 1. TAMBAHKAN KOLOM status_diagnosis PADA QUERY INSERT
                    string queryRecord = "INSERT INTO records (pasien_id, pasien_name, pasien_gender, pasien_born, record_at, user_id, status_diagnosis) " +
                                         "VALUES (@p_id, @p_name, @p_gender, @p_born, @rec_time, @u_id, @status_diag)";

                    using (MySqlCommand cmd = new MySqlCommand(queryRecord, conn))
                    {
                        cmd.Parameters.AddWithValue("@p_id", txtPasienId.Text.Trim());
                        cmd.Parameters.AddWithValue("@p_name", txtPasienName.Text.Trim());
                        cmd.Parameters.AddWithValue("@p_gender", cmbGender.Text == "Laki-Laki" ? "L" : "P");
                        cmd.Parameters.AddWithValue("@p_born", dtpBorn.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@rec_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@u_id", Session.UserId);

                        // 2. AMBIL HASIL DIAGNOSIS SECARA REAL-TIME DARI LAYAR MONITORING!
                        string diagnosisTerakhir = lblStatus != null ? lblStatus.Text : "SINUS NORMAL";
                        cmd.Parameters.AddWithValue("@status_diag", diagnosisTerakhir);

                        cmd.ExecuteNonQuery();
                    }

                    newRecordId = new MySqlCommand("SELECT LAST_INSERT_ID()", conn).ExecuteScalar() != null ?
                                  Convert.ToInt64(new MySqlCommand("SELECT LAST_INSERT_ID()", conn).ExecuteScalar()) : 0;

                    string queryData = "INSERT INTO records_datas (record_id, data_1, data_2, data_3, data_4, data_5, data_6, data_bpm) " +
                                       "VALUES (@r_id, @d1, @d2, @d3, @d4, @d5, @d6, @dBpm)";
                    using (MySqlCommand cmdData = new MySqlCommand(queryData, conn))
                    {
                        cmdData.Parameters.AddWithValue("@r_id", newRecordId);
                        cmdData.Parameters.AddWithValue("@d1", json1);
                        cmdData.Parameters.AddWithValue("@d2", json2);
                        cmdData.Parameters.AddWithValue("@d3", json3);
                        cmdData.Parameters.AddWithValue("@d4", json4);
                        cmdData.Parameters.AddWithValue("@d5", json5);
                        cmdData.Parameters.AddWithValue("@d6", json6);
                        cmdData.Parameters.AddWithValue("@dBpm", jsonBpm);

                        cmdData.CommandTimeout = 120;
                        cmdData.ExecuteNonQuery();
                    }
                }

                int detikBerjalan = (int)(DateTime.Now - startTime).TotalSeconds;
                btnAutoEcg.Text = $"REC: {detikBerjalan}s / {recordTime}s";
                MessageBox.Show("Perekaman Selesai!\nData berhasil disimpan ke Database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnAutoEcg.Text = "AUTO ECG";
                btnAutoEcg.Enabled = true;
                btnAutoEcg.BackColor = System.Drawing.Color.Navy;

                txtPasienId.Clear();
                txtPasienName.Clear();

                // =========================================================================
                // PERBAIKAN KRITIS: MATIKAN TIMER SEBELUM MEMBUKA RECORDFORM!
                // =========================================================================
                timerGambar.Stop();
                if (portArduino != null && portArduino.IsOpen) portArduino.DataReceived -= PortArduino_DataReceived;

                this.Hide();
                RecordForm recForm = new RecordForm(newRecordId);
                recForm.ShowDialog();
                this.Show();

                // Nyalakan kembali setelah RecordForm ditutup
                if (portArduino != null && portArduino.IsOpen) portArduino.DataReceived += PortArduino_DataReceived;
                timerGambar.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data rekaman: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAutoEcg.Text = "AUTO ECG";
                btnAutoEcg.Enabled = true;
                btnAutoEcg.BackColor = System.Drawing.Color.Yellow;
            }
        }

        // =========================================================================
        // 5. RENDER GRAFIK DENGAN ANTI-CRASH SHIELD (100% AMAN DARI OVERLOAD)
        // =========================================================================
        private void TimerGambar_Tick(object sender, EventArgs e)
        {
            // Jika SkiaSharp masih sibuk menggambar frame sebelumnya, LOMPATI tick ini!
            if (isRendering || this.IsDisposed || !this.Visible) return;

            try
            {
                isRendering = true; // Kunci gerbang render

                if (stream1.HasNewData)
                {
                    if (formsPlot1 != null) formsPlot1.Refresh();
                    if (formsPlot2 != null) formsPlot2.Refresh();
                    if (formsPlot3 != null) formsPlot3.Refresh();
                    if (formsPlot4 != null) formsPlot4.Refresh();
                    if (formsPlot5 != null) formsPlot5.Refresh();
                    if (formsPlot6 != null) formsPlot6.Refresh();
                }

                lblBPM.Text = $"{currentBpm:F0}";

                if (isRecording)
                {
                    int detikBerjalan = (int)(DateTime.Now - startTime).TotalSeconds;
                    btnAutoEcg.Text = $"REC: {detikBerjalan}s / {recordTime}s";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Glitch Render Terlewati: " + ex.Message);
            }
            finally
            {
                isRendering = false; // Buka kembali gerbang untuk tick berikutnya
            }
        }

        // --- 6. LOGOUT & CLOSE APLIKASI ---
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // PERBAIKAN: Matikan timer dan putus event port sebelum logout
            timerGambar.Stop();
            if (portArduino != null && portArduino.IsOpen)
            {
                portArduino.DataReceived -= PortArduino_DataReceived;
                portArduino.Close();
            }

            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        // --- 8. EVENT: TOMBOL AUTO ECG (START / CANCEL RECORD) ---
        private void btnAutoEcg_Click(object sender, EventArgs e)
        {
            // =============================================================
            // KONDISI 1: JIKA SEDANG MEREKAM -> TOMBOL BERFUNGSI SEBAGAI CANCEL
            // =============================================================
            if (isRecording)
            {
                // 1. Hentikan proses rekam
                isRecording = false;

                // 2. Kosongkan memori sementara list rekaman
                if (rec1 != null) { rec1.Clear(); rec2.Clear(); rec3.Clear(); rec4.Clear(); rec5.Clear(); rec6.Clear(); recBpm.Clear(); }

                // 3. Kembalikan tampilan tombol ke kondisi awal (AUTO ECG)
                btnAutoEcg.Text = "AUTO ECG";
                btnAutoEcg.BackColor = System.Drawing.Color.Navy; // Atau warna default tombolmu
                btnAutoEcg.Enabled = true;

                // 4. Beri informasi bahwa rekaman dibatalkan
                MessageBox.Show("Perekaman EKG dibatalkan oleh pengguna.\nData tidak disimpan ke database.",
                                "Rekaman Dibatalkan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // =============================================================
            // KONDISI 2: JIKA TIDAK MEREKAM -> MULAI REKAM BARU
            // =============================================================
            if (string.IsNullOrWhiteSpace(txtPasienId.Text) || string.IsNullOrWhiteSpace(txtPasienName.Text))
            {
                MessageBox.Show("Silakan isi Pasien ID dan Pasien Name terlebih dahulu!", "Data Belum Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Inisialisasi List penampung data
            rec1 = new List<double>();
            rec2 = new List<double>(); rec3 = new List<double>();
            rec4 = new List<double>(); rec5 = new List<double>(); rec6 = new List<double>();
            recBpm = new List<double>();

            startTime = DateTime.Now; // Catat waktu mulai

            // PENTING: Tombol TETAP AKTIF (Enabled = true) agar bisa diklik lagi untuk Cancel!
            btnAutoEcg.Enabled = true;
            btnAutoEcg.Text = "CANCEL REC (0s)";
            btnAutoEcg.BackColor = System.Drawing.Color.Crimson; // Ubah ke warna Merah sebagai tanda tombol batal

            isRecording = true;
        }


        // =========================================================================
        // PENCEGAHAN FATAL CRASH 0xc0000409 (MENGGANTIKAN OnFormClosed)
        // =========================================================================
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 1. Kunci gerbang render
            isRendering = true;

            // 2. Matikan Timer
            if (timerGambar != null)
            {
                timerGambar.Stop();
                timerGambar.Tick -= TimerGambar_Tick;
                timerGambar.Dispose();
                timerGambar = null;
            }
            if (timerWaktu != null)
            {
                timerWaktu.Stop();
                timerWaktu.Dispose();
                timerWaktu = null;
            }

            // 3. Putus & Tutup Port Arduino secara aman
            try
            {
                if (portArduino != null && portArduino.IsOpen)
                {
                    portArduino.DataReceived -= PortArduino_DataReceived;
                    portArduino.Close();
                    portArduino.Dispose();
                }
            }
            catch { }

            // 4. Kosongkan memori SkiaSharp
            try
            {
                if (formsPlot1 != null) formsPlot1.Plot.Clear();
                if (formsPlot2 != null) formsPlot2.Plot.Clear();
                if (formsPlot3 != null) formsPlot3.Plot.Clear();
                if (formsPlot4 != null) formsPlot4.Plot.Clear();
                if (formsPlot5 != null) formsPlot5.Plot.Clear();
                if (formsPlot6 != null) formsPlot6.Plot.Clear();
            }
            catch { }

            // 5. Beri waktu 50 ms bagi kartu grafis untuk menyelesaikan sisa antrean kerja
            System.Threading.Thread.Sleep(50);

            base.OnFormClosing(e);
            Application.Exit(); // Tutup aplikasi sepenuhnya
        }
        // =========================================================================
        // EKSEKUSI MACHINE LEARNING REAL-TIME (DENGAN FILTER STABILIZER / ANTI-FLICKER)
        // =========================================================================
        private void TimerAI_Tick(object sender, EventArgs e)
        {
            // ML LAMA DITIDURKAN SEMENTARA UNTUK PERSIAPAN CNN
            //return;

            if (isRendering || this.IsDisposed || !this.Visible) return;

            if (bufferLead2Realtime.Count < 1250)
            {
                if (lblStatus != null) lblStatus.Text = "MENGUMPULKAN DATA...";
                return;
            }

            try
            {
                double[] dataSiapAnalisa;
                lock (bufferLead2Realtime)
                {
                    dataSiapAnalisa = bufferLead2Realtime.ToArray();
                }

                // 1. Copy data secepat kilat (Menghindari Analisa Error)
                List<double> dataAman;
                try
                {
                    dataAman = bufferLead2Realtime
                                .Skip(Math.Max(0, bufferLead2Realtime.Count - 1250))
                                .ToList();
                }
                catch { return; }

               
                // Kita masukkan currentBpm (BPM langsung dari alat) ke dalam fungsinya
                string diagnosisMentah = ECGRuleBase.AnalisaSinyal(dataAman, currentBpm);
                if (diagnosisMentah != "MENGUMPULKAN DATA..." && diagnosisMentah != "ANALISA ERROR")
                {
                    // =====================================================================
                    // --- 3. FITUR HYBRID (Cross-check AI dengan Variabilitas BPM C#) ---
                    // =====================================================================

                    // Simpan history BPM selama 10 detik terakhir
                    historyBpmAI.Add(currentBpm);
                    if (historyBpmAI.Count > 10) historyBpmAI.RemoveAt(0);

                    // Jika sejarah BPM sudah cukup terkumpul (minimal 5 detik)
                    if (historyBpmAI.Count >= 5)
                    {
                        double maxBpm = historyBpmAI.Max();
                        double minBpm = historyBpmAI.Min();
                        double selisihBpm = maxBpm - minBpm; // Seberapa liar BPM-nya goyang?
                    }
                    // =====================================================================

                    // 4. Masukkan hasil akhir ke dalam sistem Majority Vote (Penstabil Layar)
                    historyPrediksi.Add(diagnosisMentah);
                    if (historyPrediksi.Count > 5) historyPrediksi.RemoveAt(0);

                    string hasilStabil = historyPrediksi.GroupBy(v => v)
                                                        .OrderByDescending(g => g.Count())
                                                        .First().Key;

                    if (lblStatus != null)
                    {
                        lblStatus.Text = hasilStabil;
                        //  AturWarnaLabelStatus(lblStatus, hasilStabil);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("AI Monitoring Error: " + ex.Message);
            }
        }

        // --- FUNGSI BANTUAN PEWARNA LABEL STATUS ---
        private void AturWarnaLabelStatus(Label lbl, string diagnosis)
        {
            if (diagnosis == "SINUS NORMAL")
            {
                lbl.BackColor = System.Drawing.Color.LightGreen;
                lbl.ForeColor = System.Drawing.Color.DarkGreen;
            }
            else if (diagnosis == "SINUS ARRHYTHMIA" || diagnosis == "SINUS BRADYCARDIA" || diagnosis == "SINUS TACHYCARDIA")
            {
                lbl.BackColor = System.Drawing.Color.Khaki;
                lbl.ForeColor = System.Drawing.Color.DarkGoldenrod;
            }
            else if (diagnosis == "MENGUMPULKAN DATA..." || diagnosis == "SINYAL TIDAK TERBACA")
            {
                lbl.BackColor = System.Drawing.Color.LightGray;
                lbl.ForeColor = System.Drawing.Color.Black;
            }
            else // ATRIAL FIBRILLATION & VENTRICULAR FIBRILLATION (BAHAYA!)
            {
                lbl.BackColor = System.Drawing.Color.Crimson;
                lbl.ForeColor = System.Drawing.Color.White;
            }
        }
    }
}