# Aplikasi Monitoring EKG 6-Lead (AppRakaECG6Lead)

Aplikasi desktop berbasis Windows untuk memonitor, merekam, dan mengekspor hasil pembacaan sensor Elektrokardiogram (EKG) 6-Lead ke format PDF secara *real-time* dan auto deteksi kelainan menggunakan metode rulebase.

## 1. Persiapan Perangkat Lunak (Prerequisites)
Sebelum menjalankan aplikasi, pastikan komputer atau laptop Anda sudah terinstal perangkat lunak pendukung berikut. Unduh dan instal secara berurutan:

1. **XAMPP** (Untuk menjalankan *database* MySQL secara lokal)
   * [Download XAMPP for Windows](https://www.apachefriends.org/download.html)
2. **.NET Framework 4.8 Runtime** (Mesin utama untuk menjalankan aplikasi berbasis C#)
   * [Download .NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
3. **Microsoft Visual C++ Redistributable 2015-2022** (Wajib diinstal agar grafik ScottPlot/SkiaSharp tidak mengalami *error* `ScottPlot.Fonts` / *crash*)
   * [Download VC++ Redistributable (x64)](https://aka.ms/vs/17/release/vc_redist.x64.exe)
   * [Download VC++ Redistributable (x86/32-bit)](https://aka.ms/vs/17/release/vc_redist.x86.exe)

---

## 2. Setup Database MySQL (Import raka.sql)
Aplikasi ini menyimpan data pengguna, rekam medis pasien, dan koordinat gelombang EKG di dalam *database* MySQL lokal.

1. Buka aplikasi **XAMPP Control Panel**.
2. Klik tombol **Start** pada baris **Apache** dan **MySQL** (Pastikan tulisan berubah menjadi hijau).
3. Buka browser (Chrome/Edge/Firefox) dan buka alamat URL berikut: `http://localhost/phpmyadmin/`
4. Pada panel sebelah kiri phpMyAdmin, klik menu **New** (Baru).
5. Pada kolom *Database name*, ketik nama *database* sesuai pengaturan kode Anda (contoh: `raka_ecg`), lalu klik tombol **Create** (Buat).
6. Setelah *database* berhasil dibuat, klik nama *database* tersebut pada panel sebelah kiri.
7. Klik tab **Import** di deretan menu bagian atas.
8. Klik tombol **Choose File** (Pilih File) dan cari file **`raka.sql`** dari repositori ini.
9. *Scroll* ke halaman paling bawah, lalu klik tombol **Import** (Kirim/Go).
10. Tunggu hingga muncul kotak notifikasi berwarna hijau yang menandakan *database* berhasil dimasukkan.

---

## 3. Cara Menjalankan Aplikasi (Versi Portable)
Aplikasi ini di- *build* secara *portable*, sehingga Anda tidak perlu melakukan instalasi (`setup.exe`) yang rumit ke dalam sistem Windows.

1. Pastikan XAMPP (Apache & MySQL) dalam keadaan menyala (**Start**).
2. Unduh atau ekstrak folder `.zip` aplikasi *portable* (biasanya bernama folder `Release`).
3. Masuk ke dalam folder tersebut dan cari file aplikasi utama bernama **`AppRakaECG6Lead.exe`**.
4. Klik ganda (*double-click*) pada file tersebut.
5. Login menggunakan *username* dan *password* yang sudah ada di dalam *database*, lalu aplikasi siap digunakan.

---

## 4. Troubleshooting Perangkat Keras (Hardware)
* **Port Alat Tidak Terbaca:** Pastikan kabel USB dari alat (Arduino/ESP) sudah tertancap ke komputer. Jika masih tidak muncul di menu *Setting* aplikasi, Anda perlu menginstal *Driver* USB **CH340** atau **FTDI** bawaan mikrokontroler.
* **Gagal Menyimpan Rekaman:** Pastikan koneksi ke *database* MySQL via XAMPP tidak terputus saat proses perekaman (Auto ECG) sedang berjalan.# Markdown syntax guide