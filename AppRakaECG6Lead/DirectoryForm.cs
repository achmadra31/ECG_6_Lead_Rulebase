using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AppRakaECG6Lead
{
    // =========================================================================
    // KELAS DIRECTORY FORM (MANAJEMEN & RIWAYAT REKAMAN EKG)
    // -------------------------------------------------------------------------
    // Deskripsi : Berfungsi sebagai pusat arsip untuk melihat, mencari, 
    //             membuka kembali (Playback), dan menghapus riwayat pemeriksaan 
    //             pasien yang tersimpan di database MySQL.
    // =========================================================================
    public partial class DirectoryForm : Form
    {
        // Variabel penyimpan state: Menampung ID unik rekaman yang sedang diklik/dipilih oleh user di tabel
        private long selectedRecordId = 0;

        public DirectoryForm()
        {
            InitializeComponent();
        }

        // --- 1. EVENT: SAAT FORM DIMUAT ---
        private void DirectoryForm_Load(object sender, EventArgs e)
        {
            LoadDataRecords(""); // Memuat seluruh data riwayat secara otomatis tanpa filter pencarian saat pertama kali dibuka
        }

        // --- 2. FUNGSI: MENAMPILKAN DATA KE TABEL (DATAGRIDVIEW) ---
        private void LoadDataRecords(string keyword)
        {
            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();
                    // Query SQL: Menggabungkan (JOIN) tabel 'records' dan 'users' 
                    // agar informasi identitas pasien terhubung dengan nama petugas medis yang merekamnya.
                    string query = @"SELECT 
                                        r.record_id AS 'ID Rekaman', 
                                        r.pasien_id AS 'No. RM', 
                                        r.pasien_name AS 'Nama Pasien', 
                                        r.pasien_gender AS 'L/P', 
                                        r.status_diagnosis AS 'Status Diagnosa', 
                                        DATE_FORMAT(r.pasien_born, '%d-%m-%Y') AS 'Tgl Lahir', 
                                        DATE_FORMAT(r.record_at, '%d-%m-%Y %H:%i:%s') AS 'Waktu Rekam', 
                                        u.fullname AS 'Petugas'
                                     FROM records r
                                     LEFT JOIN users u ON r.user_id = u.user_id";

                    // Fitur Pencarian Dinamis: Menambahkan filter SQL jika user mengetik nama atau ID pasien
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " WHERE r.pasien_name LIKE @key OR r.pasien_id LIKE @key";
                    }

                    query += " ORDER BY r.record_at DESC"; // Pengurutan: Menampilkan data terbaru di baris paling atas

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            // Parameterisasi SQL (Mencegah celah SQL Injection pada kolom pencarian)
                            cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvRecords.DataSource = dt;

                        // Menyembunyikan kolom ID Rekaman agar tampilan tabel bersih dari angka database yang kaku
                        if (dgvRecords.Columns.Contains("ID Rekaman"))
                        {
                            dgvRecords.Columns["ID Rekaman"].Visible = false;
                        }
                    }
                }

                // Reset pilihan setiap kali tabel dimuat ulang
                selectedRecordId = 0;
                dgvRecords.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data direktori: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 3. EVENT: TOMBOL PENCARIAN ---
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Memanggil fungsi LoadDataRecords dengan membawa teks yang diketik di kotak pencarian
            LoadDataRecords(txtSearch.Text.Trim());
        }

        // --- 4. EVENT: KLIK BARIS DI TABEL (CELL CLICK) ---
        private void dgvRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validasi: Memastikan yang diklik adalah baris data yang valid (bukan header/judul kolom)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRecords.Rows[e.RowIndex];

                // Mengambil dan menyimpan ID Rekaman tersembunyi dari baris yang dipilih
                if (row.Cells["ID Rekaman"].Value != null && row.Cells["ID Rekaman"].Value != DBNull.Value)
                {
                    selectedRecordId = Convert.ToInt64(row.Cells["ID Rekaman"].Value);
                }
            }
        }

        // --- 5. EVENT: TOMBOL BUKA & CETAK (PLAYBACK) ---
        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Validasi: Memastikan user sudah memilih salah satu pasien di tabel sebelum menekan tombol Buka
            if (selectedRecordId == 0)
            {
                MessageBox.Show("Pilih salah satu data pasien dari tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Hide(); // Menyembunyikan DirectoryForm sementara

            // Membuka RecordForm (Halaman pemutaran ulang grafik / Playback & Cetak PDF) berdasarkan ID rekaman terpilih
            RecordForm record = new RecordForm(selectedRecordId);
            record.ShowDialog();

            this.Show(); // Memunculkan kembali DirectoryForm setelah user keluar dari RecordForm
        }

        // --- 6. EVENT: TOMBOL HAPUS (DELETE) ---
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Validasi: Memastikan ada data yang dipilih
            if (selectedRecordId == 0)
            {
                MessageBox.Show("Pilih salah satu data pasien dari tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Dialog konfirmasi pengamanan agar user tidak tidak sengaja menghapus data penting
            DialogResult dialog = MessageBox.Show("Apakah Anda yakin ingin menghapus riwayat EKG ini secara permanen?\n\nSemua grafik JSON yang terkait juga akan terhapus.", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = KoneksiDB.GetConnection())
                    {
                        conn.Open();

                        // Langkah A: Menghapus data detail sinyal JSON terlebih dahulu dari tabel relasi 'records_datas'
                        string deleteData = "DELETE FROM records_datas WHERE record_id = @id";
                        using (MySqlCommand cmd = new MySqlCommand(deleteData, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedRecordId);
                            cmd.ExecuteNonQuery();
                        }

                        // Langkah B: Menghapus data utama identitas pasien dari tabel 'records'
                        string deleteRecord = "DELETE FROM records WHERE record_id = @id";
                        using (MySqlCommand cmd = new MySqlCommand(deleteRecord, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedRecordId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataRecords(txtSearch.Text.Trim()); // Refresh tabel secara otomatis setelah penghapusan
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- 7. EVENT: TOMBOL KEMBALI ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Menutup form direktori dan kembali ke menu sebelumnya
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}