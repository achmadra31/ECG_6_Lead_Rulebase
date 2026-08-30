using System;
using System.IO.Ports;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AppRakaECG6Lead
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        // EVENT: Saat form pertama kali dimuat
        private void SettingForm_Load(object sender, EventArgs e)
        {
            // 1. Cek Hak Akses (Sembunyikan tombol jika bukan admin)
            if (Session.Role != "admin")
            {
                btnUserAccess.Visible = false;
            }

            // 2. Deteksi otomatis COM Port yang tertancap di PC
            string[] ports = SerialPort.GetPortNames();
            cmbCom.Items.AddRange(ports);

            // 3. Ambil data pengaturan terakhir dari Database
            MuatPengaturanDariDB();
        }

        private void MuatPengaturanDariDB()
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
                            string dbCom = reader["setting_com"].ToString();
                            txtRecordTime.Text = reader["setting_recordtime"].ToString();

                            // Set nilai ComboBox sesuai database jika port-nya tersedia
                            if (cmbCom.Items.Contains(dbCom))
                            {
                                cmbCom.SelectedItem = dbCom;
                            }
                            else
                            {
                                cmbCom.Text = dbCom; // Tetap tampilkan meski port sedang tidak tertancap
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat pengaturan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENT: Saat tombol SAVE diklik
        private void btnSave_Click(object sender, EventArgs e)
        {
            string comBaru = cmbCom.Text.Trim();
            string waktuBaru = txtRecordTime.Text.Trim();

            // Validasi Input
            if (string.IsNullOrEmpty(comBaru) || string.IsNullOrEmpty(waktuBaru))
            {
                MessageBox.Show("Semua kolom harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(waktuBaru, out _))
            {
                MessageBox.Show("Lama rekam harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update ke Database
            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE settings SET setting_com = @com, setting_recordtime = @waktu";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@com", comBaru);
                        cmd.Parameters.AddWithValue("@waktu", waktuBaru);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Pengaturan berhasil disimpan!\n\nAplikasi akan memuat ulang koneksi alat.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Tutup form setting setelah save
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENT: Saat tombol USER ACCESS diklik
        private void btnUserAccess_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.ShowDialog();
        }
    }
}