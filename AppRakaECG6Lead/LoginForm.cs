using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace AppRakaECG6Lead
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // Fungsi ketika tombol kuning "LOGIN" diklik
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            // 1. Validasi Input Kosong
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Proses Cek ke Database
            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();

                    // Menggunakan Parameter (@user, @pass) sangat penting untuk mencegah Hacking/SQL Injection
                    string query = "SELECT * FROM users WHERE username = @user AND password = @pass";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pass);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Jika data ditemukan
                            {
                                // Simpan data user ke Session global
                                Session.UserId = Convert.ToInt32(reader["user_id"]);
                                Session.Username = reader["username"].ToString();
                                Session.Fullname = reader["fullname"].ToString();
                                Session.Role = reader["role"].ToString();

                                MessageBox.Show($"Login Berhasil!\nSelamat datang, {Session.Fullname}.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Pindah ke MonitoringForm
                                MonitoringForm mainForm = new MonitoringForm();
                                mainForm.Show();

                                // Sembunyikan form login ini
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Username atau Password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Jika XAMPP belum dinyalakan, errornya muncul di sini
                MessageBox.Show("Koneksi Database Gagal!\nPastikan XAMPP (MySQL) sudah menyala.\n\nDetail: " + ex.Message,
                                "Koneksi Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Opsional: Tutup aplikasi sepenuhnya jika form login disilang (X)
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void lblJudul_Click(object sender, EventArgs e)
        {

        }
    }
}