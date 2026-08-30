using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AppRakaECG6Lead
{
    public partial class UserForm : Form
    {
        // Variabel untuk menyimpan ID user yang sedang diklik
        private int selectedUserId = 0;

        public UserForm()
        {
            InitializeComponent();
        }

        // --- 1. SAAT FORM DIMUAT ---
        private void UserForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("admin");
            cmbRole.Items.Add("user");
            cmbRole.SelectedIndex = 1;

            // Memaksa pengaturan tabel agar aman dan mudah diklik
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.MultiSelect = false; // Hanya bisa pilih 1 baris

            LoadDataUsers();
        }

        private void LoadDataUsers()
        {
            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT user_id AS 'ID', fullname AS 'Nama Lengkap', username AS 'Username', role AS 'Hak Akses' FROM users";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvUsers.DataSource = dt;
                    }
                }

                // Kosongkan pilihan dan inputan setelah memuat data
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // FUNGSI BANTUAN: Membersihkan kolom input
        private void ClearInputs()
        {
            txtFullname.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = 1;
            selectedUserId = 0; // Reset ID
            dgvUsers.ClearSelection();
        }

        // --- 2. EVENT: KLIK BARIS DI TABEL ---
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Pastikan yang diklik adalah baris data yang valid (bukan header/judul kolom)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

                // Ambil data dari tabel dan masukkan ke TextBox
                selectedUserId = Convert.ToInt32(row.Cells["ID"].Value);
                txtFullname.Text = row.Cells["Nama Lengkap"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                cmbRole.Text = row.Cells["Hak Akses"].Value.ToString();

                // Kosongkan password. Biarkan user mengetik baru JIKA ingin diubah
                txtPassword.Clear();
            }
        }

        // --- 3. EVENT: TOMBOL CREATE ---
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.Text;

            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Semua kolom (Nama, Username, Password, Role) harus diisi untuk user baru!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @user";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@user", username);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Username sudah digunakan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO users (fullname, username, password, role) VALUES (@nama, @user, @pass, @role)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", fullname);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("User berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 4. EVENT: TOMBOL UPDATE ---
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Pilih user dari tabel terlebih dahulu yang ingin diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fullname = txtFullname.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.Text;

            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Nama, Username, dan Role tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = KoneksiDB.GetConnection())
                {
                    conn.Open();

                    // Logika Update: Cek apakah kolom password diisi atau dibiarkan kosong
                    string updateQuery;
                    if (string.IsNullOrEmpty(password))
                    {
                        // Jika password kosong, jangan ubah password di database
                        updateQuery = "UPDATE users SET fullname = @nama, username = @user, role = @role WHERE user_id = @id";
                    }
                    else
                    {
                        // Jika password diisi, ubah semuanya termasuk password
                        updateQuery = "UPDATE users SET fullname = @nama, username = @user, password = @pass, role = @role WHERE user_id = @id";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedUserId);
                        cmd.Parameters.AddWithValue("@nama", fullname);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@role", role);

                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("@pass", password);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data user berhasil di-update!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal meng-update user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 5. EVENT: TOMBOL DELETE ---
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedUserId == 0)
            {
                MessageBox.Show("Pilih user dari tabel terlebih dahulu yang ingin dihapus!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectedUserId == Session.UserId)
            {
                MessageBox.Show("Anda tidak bisa menghapus akun yang sedang Anda gunakan saat ini!", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult dialog = MessageBox.Show($"Apakah Anda yakin ingin menghapus user '{txtFullname.Text}'?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = KoneksiDB.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM users WHERE user_id = @id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedUserId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("User berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}