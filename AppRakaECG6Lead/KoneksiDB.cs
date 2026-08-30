using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace AppRakaECG6Lead
{
    // === CLASS KONEKSI DATABASE ===
    public class KoneksiDB
    {
        // Sesuaikan dengan pengaturan XAMPP bawaan (username: root, password kosong)
        private static string connectionString = "Server=127.0.0.1;Database=raka;Uid=root;Pwd=root;";

        // Fungsi untuk memanggil koneksi dengan mudah dari form mana saja
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }

    // === CLASS SESSION (Mengingat User yang Login) ===
    public static class Session
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Fullname { get; set; }
        public static string Role { get; set; } // 'admin' atau 'user'
    }
}