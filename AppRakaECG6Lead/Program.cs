using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace AppRakaECG6Lead
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 1. MENANGKAP ERROR DARI THREAD UI UTAMA (WINFORMS)
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalUIExceptionHandler);

            // 2. MENANGKAP ERROR DARI BACKGROUND THREAD (SERIAL PORT / TIMER / DB ASYNC)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalBackgroundExceptionHandler);

            // 3. MENCEGAH CRASH PADA ENGINE RENDER
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ganti 'MainForm' atau 'DirectoryForm' di bawah ini sesuai dengan form pertama yang biasa kamu jalankan saat aplikasi dibuka
            Application.Run(new LoginForm());
        }

        // --- FUNGSI PENANGKAP ERROR UI UTAMA ---
        static void GlobalUIExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            TampilkanDanCatatError("UI Thread Exception", e.Exception);
        }

        // --- FUNGSI PENANGKAP ERROR BACKGROUND THREAD ---
        static void GlobalBackgroundExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            TampilkanDanCatatError("Background Thread Exception", ex);
        }

        // --- FUNGSI PENCATAT & PENAMPIL ERROR KE LAYAR ---
        static void TampilkanDanCatatError(string tipeError, Exception ex)
        {
            // Buat pesan error yang sangat detail untuk developer
            string pesanError = $"TERJADI KESALAHAN SISTEM ({tipeError}):\n\n" +
                                $"Pesan: {ex.Message}\n\n" +
                                $"Lokasi Baris Kode (StackTrace):\n{ex.StackTrace}";

            // 1. Simpan ke file teks crash_log.txt di folder aplikasi agar bisa dibaca ulang
            try
            {
                string logPath = Path.Combine(Application.StartupPath, "crash_log.txt");
                File.AppendAllText(logPath, $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {pesanError}\n------------------------------------------------\n\n");
            }
            catch { /* Abaikan jika gagal menulis log */ }

            // 2. Tampilkan pesan ke layar tanpa menutup aplikasi!
            MessageBox.Show(pesanError + "\n\nError ini telah dicatat di file crash_log.txt.",
                            "Peringatan Crash - Sistem Tetap Berjalan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
    }
}