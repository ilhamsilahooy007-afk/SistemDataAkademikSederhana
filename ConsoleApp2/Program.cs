using SistemDataAkademik.Models;
using System.Globalization;
using System.Text;

namespace SiakadConsole;

internal class Program
{
    private static readonly List<Prodi> DaftarProdi = new();
    private static readonly List<Mahasiswa> DaftarMahasiswa = new();
    private static readonly List<MataKuliah> DaftarMataKuliah = new();
    private static readonly List<Dosen> DaftarDosen = new();

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.Clear();
            Judul("SISTEM AKADEMIK");
            Console.WriteLine("1. Prodi");
            Console.WriteLine("2. Mahasiswa");
            Console.WriteLine("3. Mata Kuliah");
            Console.WriteLine("4. Dosen");
            Console.WriteLine("5. Keluar");
            Console.Write("\nPilih menu: ");

            switch (Console.ReadLine())
            {
                case "1": MenuProdi(); break;
                case "2": MenuMahasiswa(); break;
                case "3": MenuMataKuliah(); break;
                case "4": MenuDosen(); break;
                case "5":
                    Console.WriteLine("\nTerima kasih telah menggunakan Sistem Akademik.");
                    return;
                default: Pesan("Pilihan tidak tersedia. Masukkan angka 1 sampai 5."); break;
            }
        }
    }



    private static void Judul(string teks)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine($"                 {teks}");
        Console.WriteLine("==================================================");
    }

    private static void Pesan(string teks)
    {
        Console.WriteLine("\n" + teks);
        Console.WriteLine("Tekan tombol apa saja untuk melanjutkan...");
        Console.ReadKey(true);
    }

    private static string BacaTeks(string pesan)
    {
        Console.Write(pesan);
        return (Console.ReadLine() ?? "").Trim();
    }

    private static bool HanyaAngka(string teks) =>
        !string.IsNullOrEmpty(teks) && teks.All(c => c >= '0' && c <= '9');

    private static bool HurufDanSpasi(string teks) =>
        !string.IsNullOrWhiteSpace(teks) &&
        teks.All(c => char.IsLetter(c) || c == ' ');

    private static bool NamaMataKuliahValid(string teks) =>
        !string.IsNullOrWhiteSpace(teks) &&
        teks.All(c => char.IsLetterOrDigit(c) || c == ' ');

    private static string BacaNama(string label, int minimum = 5, int maksimum = 50)
    {
        while (true)
        {
            string nilai = BacaTeks(label);
            if (nilai.Length < minimum || nilai.Length > maksimum || !HurufDanSpasi(nilai))
                Console.WriteLine($"Nama harus {minimum}-{maksimum} karakter dan hanya boleh berisi huruf serta spasi.");
            else
                return nilai;
        }
    }

    private static string BacaIdentitasAngka(string label, int jumlahDigit)
    {
        while (true)
        {
            string nilai = BacaTeks(label);
            if (nilai.Length != jumlahDigit || !HanyaAngka(nilai))
                Console.WriteLine($"Isian harus tepat {jumlahDigit} digit angka (0-9), tanpa spasi atau simbol.");
            else
                return nilai;
        }
    }

    private static bool BacaJenisKelamin()
    {
        while (true)
        {
            string nilai = BacaTeks("Jenis kelamin (L/P): ").ToUpperInvariant();
            if (nilai == "L") return true;
            if (nilai == "P") return false;
            Console.WriteLine("Masukkan L untuk Laki-laki atau P untuk Perempuan.");
        }
    }

    private static string TeksJenisKelamin(bool nilai) =>
        nilai ? "Laki-laki" : "Perempuan";

    private static bool KonfirmasiHapus()
    {
        string jawaban = BacaTeks("Yakin menghapus data ini? (Y/T): ").ToUpperInvariant();
        return jawaban == "Y";
    }

    private static Prodi PilihProdi()
    {
        if (DaftarProdi.Count == 0)
        {
            Console.WriteLine("Data prodi belum tersedia. Tambahkan prodi terlebih dahulu.");
            return null;
        }

        Console.WriteLine("\nDaftar Prodi:");
        for (int i = 0; i < DaftarProdi.Count; i++)
            Console.WriteLine($"{i + 1}. {DaftarProdi[i].KodeProdi} - {DaftarProdi[i].NamaProdi}");

        while (true)
        {
            string input = BacaTeks("Pilih nomor prodi: ");
            if (int.TryParse(input, out int nomor) && nomor >= 1 && nomor <= DaftarProdi.Count)
                return DaftarProdi[nomor - 1];

            Console.WriteLine("Nomor prodi tidak valid.");
        }
    }

    private static Dosen PilihDosen()
    {
        if (DaftarDosen.Count == 0)
        {
            Console.WriteLine("Data dosen belum tersedia. Tambahkan dosen terlebih dahulu.");
            return null;
        }

        Console.WriteLine("\nDaftar Dosen:");
        for (int i = 0; i < DaftarDosen.Count; i++)
            Console.WriteLine($"{i + 1}. {DaftarDosen[i].Nidn} - {DaftarDosen[i].NamaLengkap}");

        while (true)
        {
            string input = BacaTeks("Pilih nomor dosen pembimbing: ");
            if (int.TryParse(input, out int nomor) && nomor >= 1 && nomor <= DaftarDosen.Count)
                return DaftarDosen[nomor - 1];

            Console.WriteLine("Nomor dosen tidak valid.");
        }
    }

    private static Prodi CariProdi(string kode) =>
        DaftarProdi.FirstOrDefault(p => p.KodeProdi == kode);

    private static Mahasiswa CariMahasiswa(string nim) =>
        DaftarMahasiswa.FirstOrDefault(m => m.Nim == nim);

    private static MataKuliah CariMataKuliah(string kode) =>
        DaftarMataKuliah.FirstOrDefault(m => m.KodeMataKuliah == kode);

    private static Dosen CariDosen(string nidn) =>
        DaftarDosen.FirstOrDefault(d => d.Nidn == nidn);

    private static void MenuEntitas(string nama, Action daftar, Action tambah, Action ubah, Action hapus)
    {
        while (true)
        {
            Console.Clear();
            Judul("MENU " + nama.ToUpperInvariant());
            Console.WriteLine("1. Daftar");
            Console.WriteLine("2. Tambah");
            Console.WriteLine("3. Ubah");
            Console.WriteLine("4. Hapus");
            Console.WriteLine("5. Kembali ke menu utama");
            Console.Write("\nPilih menu: ");

            switch (Console.ReadLine())
            {
                case "1": daftar(); break;
                case "2": tambah(); break;
                case "3": ubah(); break;
                case "4": hapus(); break;
                case "5": return;
                default: Pesan("Pilihan tidak tersedia."); break;
            }
        }
    }

    private static void MenuProdi() =>
        MenuEntitas("Prodi", DaftarProdiTampil, TambahProdi, UbahProdi, HapusProdi);

    private static void MenuMahasiswa() =>
        MenuEntitas("Mahasiswa", DaftarMahasiswaTampil, TambahMahasiswa, UbahMahasiswa, HapusMahasiswa);

    private static void MenuMataKuliah() =>
        MenuEntitas("Mata Kuliah", DaftarMataKuliahTampil, TambahMataKuliah, UbahMataKuliah, HapusMataKuliah);

    private static void MenuDosen() =>
        MenuEntitas("Dosen", DaftarDosenTampil, TambahDosen, UbahDosen, HapusDosen);


    private static void DaftarProdiTampil()
    {
        Console.Clear();
        Judul("DAFTAR PRODI");
        if (DaftarProdi.Count == 0) Console.WriteLine("Data belum tersedia.");
        else
        {
            Console.WriteLine("{0,-12} {1,-30} {2,-15}", "KODE", "NAMA PRODI", "ALIAS");
            Console.WriteLine(new string('-', 60));
            foreach (Prodi p in DaftarProdi)
                Console.WriteLine("{0,-12} {1,-30} {2,-15}", p.KodeProdi, p.NamaProdi, p.AliasProdi);
        }
        Console.ReadKey(true);
    }

    private static void TambahProdi()
    {
        Console.Clear();
        Judul("TAMBAH PRODI");

        string kode;
        while (true)
        {
            kode = BacaIdentitasAngka("Kode Prodi (tepat 5 digit): ", 5);
            if (CariProdi(kode) != null) Console.WriteLine("Kode prodi sudah terdaftar.");
            else break;
        }

        string nama = BacaNama("Nama Prodi (5-50 karakter): ");
        string alias;
        while (true)
        {
            alias = BacaTeks("Alias Prodi (2-20 karakter): ");
            if (alias.Length < 2 || alias.Length > 20)
                Console.WriteLine("Alias harus 2-20 karakter.");
            else break;
        }

        DaftarProdi.Add(new Prodi { KodeProdi = kode, NamaProdi = nama, AliasProdi = alias });
        Pesan("Data prodi berhasil ditambahkan.");
    }

    private static void UbahProdi()
    {
        Console.Clear();
        Judul("UBAH PRODI");
        Prodi p = CariProdi(BacaTeks("Masukkan kode prodi: "));
        if (p == null) { Pesan("Data prodi tidak ditemukan."); return; }

        Console.WriteLine($"Data lama: {p.KodeProdi} | {p.NamaProdi} | {p.AliasProdi}");
        p.NamaProdi = BacaNama("Nama Prodi baru (5-50 karakter): ");
        while (true)
        {
            string alias = BacaTeks("Alias Prodi baru (2-20 karakter): ");
            if (alias.Length < 2 || alias.Length > 20) Console.WriteLine("Alias harus 2-20 karakter.");
            else { p.AliasProdi = alias; break; }
        }
        Pesan("Data prodi berhasil diperbarui. Kode prodi tetap.");
    }

    private static void HapusProdi()
    {
        Console.Clear();
        Judul("HAPUS PRODI");
        Prodi p = CariProdi(BacaTeks("Masukkan kode prodi: "));
        if (p == null) { Pesan("Data prodi tidak ditemukan."); return; }

        bool dipakaiMahasiswa = DaftarMahasiswa.Any(m => m.KodeProdi == p.KodeProdi);
        bool dipakaiMatkul = DaftarMataKuliah.Any(m => m.KodeProdi == p.KodeProdi);
        bool dipakaiDosen = DaftarDosen.Any(d => d.KodeProdi == p.KodeProdi);
        if (dipakaiMahasiswa || dipakaiMatkul || dipakaiDosen)
        {
            Pesan("Prodi tidak dapat dihapus karena masih digunakan mahasiswa, mata kuliah, atau dosen.");
            return;
        }

        Console.WriteLine($"Prodi ditemukan: {p.NamaProdi} ({p.AliasProdi})");
        if (KonfirmasiHapus())
        {
            DaftarProdi.Remove(p);
            Pesan("Data prodi berhasil dihapus.");
        }
        else Pesan("Penghapusan dibatalkan.");
    }



    private static void DaftarDosenTampil()
    {
        Console.Clear();
        Judul("DAFTAR DOSEN");
        if (DaftarDosen.Count == 0) Console.WriteLine("Data belum tersedia.");
        else
        {
            foreach (Dosen d in DaftarDosen)
            {
                Prodi p = CariProdi(d.KodeProdi);
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"NIDN          : {d.Nidn}");
                Console.WriteLine($"Nama          : {d.NamaLengkap}");
                Console.WriteLine($"Jenis Kelamin : {TeksJenisKelamin(d.JenisKelamin)}");
                Console.WriteLine($"Tanggal Lahir : {d.TanggalLahir:dd-MM-yyyy}");
                Console.WriteLine($"Prodi         : {(p == null ? "-" : p.NamaProdi)}");
            }
        }
        Console.ReadKey(true);
    }

    private static void TambahDosen()
    {
        Console.Clear();
        Judul("TAMBAH DOSEN");
        if (DaftarProdi.Count == 0) { Pesan("Tambahkan prodi terlebih dahulu."); return; }

        string nidn;
        while (true)
        {
            nidn = BacaIdentitasAngka("NIDN (tepat 10 digit): ", 10);
            if (CariDosen(nidn) != null) Console.WriteLine("NIDN sudah terdaftar.");
            else break;
        }

        string nama = BacaNama("Nama Lengkap (5-50 karakter): ");
        bool jk = BacaJenisKelamin();
        DateTime tanggal = BacaTanggalLahir();
        Prodi prodi = PilihProdi();
        if (prodi == null) return;

        DaftarDosen.Add(new Dosen
        {
            Nidn = nidn,
            NamaLengkap = nama,
            JenisKelamin = jk,
            TanggalLahir = tanggal,
            KodeProdi = prodi.KodeProdi
        });
        Pesan("Data dosen berhasil ditambahkan.");
    }

    private static DateTime BacaTanggalLahir()
    {
        while (true)
        {
            string input = BacaTeks("Tanggal lahir (dd-MM-yyyy): ");
            if (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime tanggal) && tanggal <= DateTime.Today)
                return tanggal;

            Console.WriteLine("Tanggal tidak valid. Gunakan format dd-MM-yyyy dan tanggal tidak boleh di masa depan.");
        }
    }

    private static void UbahDosen()
    {
        Console.Clear();
        Judul("UBAH DOSEN");
        Dosen d = CariDosen(BacaTeks("Masukkan NIDN: "));
        if (d == null) { Pesan("Data dosen tidak ditemukan."); return; }

        Console.WriteLine($"Data lama: {d.Nidn} | {d.NamaLengkap} | {TeksJenisKelamin(d.JenisKelamin)} | {d.TanggalLahir:dd-MM-yyyy}");
        d.NamaLengkap = BacaNama("Nama baru (5-50 karakter): ");
        d.JenisKelamin = BacaJenisKelamin();
        d.TanggalLahir = BacaTanggalLahir();
        Prodi prodi = PilihProdi();
        if (prodi == null) return;
        d.KodeProdi = prodi.KodeProdi;
        Pesan("Data dosen berhasil diperbarui. NIDN tetap.");
    }

    private static void HapusDosen()
    {
        Console.Clear();
        Judul("HAPUS DOSEN");
        Dosen d = CariDosen(BacaTeks("Masukkan NIDN: "));
        if (d == null) { Pesan("Data dosen tidak ditemukan."); return; }

        if (DaftarMahasiswa.Any(m => m.NidnPembimbingAkademik == d.Nidn))
        {
            Pesan("Dosen tidak dapat dihapus karena masih menjadi pembimbing akademik mahasiswa.");
            return;
        }

        Console.WriteLine($"Dosen ditemukan: {d.NamaLengkap} ({d.Nidn})");
        if (KonfirmasiHapus())
        {
            DaftarDosen.Remove(d);
            Pesan("Data dosen berhasil dihapus.");
        }
        else Pesan("Penghapusan dibatalkan.");
    }



    private static void DaftarMataKuliahTampil()
    {
        Console.Clear();
        Judul("DAFTAR MATA KULIAH");
        if (DaftarMataKuliah.Count == 0) Console.WriteLine("Data belum tersedia.");
        else
        {
            foreach (MataKuliah mk in DaftarMataKuliah)
            {
                Prodi p = CariProdi(mk.KodeProdi);
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"Kode Mata Kuliah : {mk.KodeMataKuliah}");
                Console.WriteLine($"Nama             : {mk.NamaMataKuliah}");
                Console.WriteLine($"SKS              : {mk.Sks.ToString("0.##", CultureInfo.InvariantCulture)}");
                Console.WriteLine($"Prodi            : {(p == null ? "-" : p.NamaProdi)}");
            }
        }
        Console.ReadKey(true);
    }

    private static void TambahMataKuliah()
    {
        Console.Clear();
        Judul("TAMBAH MATA KULIAH");
        if (DaftarProdi.Count == 0) { Pesan("Tambahkan prodi terlebih dahulu."); return; }

        string kode;
        while (true)
        {
            kode = BacaTeks("Kode Mata Kuliah (5-10 karakter): ");
            if (kode.Length < 5 || kode.Length > 10)
                Console.WriteLine("Kode mata kuliah harus 5-10 karakter.");
            else if (CariMataKuliah(kode) != null)
                Console.WriteLine("Kode mata kuliah sudah terdaftar.");
            else break;
        }

        string nama;
        while (true)
        {
            nama = BacaTeks("Nama Mata Kuliah (5-50 karakter): ");
            if (nama.Length < 5 || nama.Length > 50 || !NamaMataKuliahValid(nama))
                Console.WriteLine("Nama harus 5-50 karakter; hanya huruf, angka, dan spasi.");
            else break;
        }

        double sks = BacaSks();
        Prodi prodi = PilihProdi();
        if (prodi == null) return;

        DaftarMataKuliah.Add(new MataKuliah
        {
            KodeMataKuliah = kode,
            NamaMataKuliah = nama,
            Sks = sks,
            KodeProdi = prodi.KodeProdi
        });
        Pesan("Data mata kuliah berhasil ditambahkan.");
    }

    private static double BacaSks()
    {
        while (true)
        {
            string input = BacaTeks("Jumlah SKS (0,1-10): ").Replace(',', '.');
            if (double.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double sks)
                && sks >= 0.1 && sks <= 10)
                return sks;

            Console.WriteLine("SKS harus berupa angka antara 0,1 sampai 10.");
        }
    }

    private static void UbahMataKuliah()
    {
        Console.Clear();
        Judul("UBAH MATA KULIAH");
        MataKuliah mk = CariMataKuliah(BacaTeks("Masukkan kode mata kuliah: "));
        if (mk == null) { Pesan("Data mata kuliah tidak ditemukan."); return; }

        Prodi prodiLama = CariProdi(mk.KodeProdi);
        Console.WriteLine($"Data lama: {mk.KodeMataKuliah} | {mk.NamaMataKuliah} | {mk.Sks} SKS | {prodiLama?.NamaProdi}");
        string nama;
        while (true)
        {
            nama = BacaTeks("Nama Mata Kuliah baru (5-50 karakter): ");
            if (nama.Length < 5 || nama.Length > 50 || !NamaMataKuliahValid(nama))
                Console.WriteLine("Nama harus 5-50 karakter; hanya huruf, angka, dan spasi.");
            else break;
        }

        mk.NamaMataKuliah = nama;
        mk.Sks = BacaSks();
        Prodi prodi = PilihProdi();
        if (prodi == null) return;
        mk.KodeProdi = prodi.KodeProdi;
        Pesan("Data mata kuliah berhasil diperbarui. Kode mata kuliah tetap.");
    }

    private static void HapusMataKuliah()
    {
        Console.Clear();
        Judul("HAPUS MATA KULIAH");
        MataKuliah mk = CariMataKuliah(BacaTeks("Masukkan kode mata kuliah: "));
        if (mk == null) { Pesan("Data mata kuliah tidak ditemukan."); return; }

        Console.WriteLine($"Mata kuliah ditemukan: {mk.NamaMataKuliah} ({mk.KodeMataKuliah})");
        if (KonfirmasiHapus())
        {
            DaftarMataKuliah.Remove(mk);
            Pesan("Data mata kuliah berhasil dihapus.");
        }
        else Pesan("Penghapusan dibatalkan.");
    }

   
    private static void DaftarMahasiswaTampil()
    {
        Console.Clear();
        Judul("DAFTAR MAHASISWA");
        if (DaftarMahasiswa.Count == 0) Console.WriteLine("Data belum tersedia.");
        else
        {
            foreach (Mahasiswa m in DaftarMahasiswa)
            {
                Prodi p = CariProdi(m.KodeProdi);
                Dosen d = CariDosen(m.NidnPembimbingAkademik);
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"NIM               : {m.Nim}");
                Console.WriteLine($"Nama              : {m.NamaLengkap}");
                Console.WriteLine($"Jenis Kelamin     : {TeksJenisKelamin(m.JenisKelamin)}");
                Console.WriteLine($"Program Studi     : {(p == null ? "-" : p.NamaProdi)}");
                Console.WriteLine($"Dosen Pembimbing  : {(d == null ? "-" : d.NamaLengkap)}");
                Console.WriteLine($"NIDN Pembimbing   : {m.NidnPembimbingAkademik}");
            }
        }
        Console.ReadKey(true);
    }

    private static void TambahMahasiswa()
    {
        Console.Clear();
        Judul("TAMBAH MAHASISWA");
        if (DaftarProdi.Count == 0) { Pesan("Tambahkan prodi terlebih dahulu."); return; }
        if (DaftarDosen.Count == 0) { Pesan("Tambahkan dosen terlebih dahulu agar dapat dipilih sebagai pembimbing."); return; }

        string nim;
        while (true)
        {
            nim = BacaIdentitasAngka("NIM (tepat 10 digit): ", 10);
            if (CariMahasiswa(nim) != null) Console.WriteLine("NIM sudah terdaftar.");
            else break;
        }

        string nama = BacaNama("Nama Lengkap (5-50 karakter): ");
        bool jk = BacaJenisKelamin();
        Prodi prodi = PilihProdi();
        if (prodi == null) return;

        Dosen dosen = PilihDosen();
        if (dosen == null) return;

        DaftarMahasiswa.Add(new Mahasiswa
        {
            Nim = nim,
            NamaLengkap = nama,
            JenisKelamin = jk,
            KodeProdi = prodi.KodeProdi,
            NidnPembimbingAkademik = dosen.Nidn
        });
        Pesan("Data mahasiswa berhasil ditambahkan.");
    }

    private static void UbahMahasiswa()
    {
        Console.Clear();
        Judul("UBAH MAHASISWA");
        Mahasiswa m = CariMahasiswa(BacaTeks("Masukkan NIM: "));
        if (m == null) { Pesan("Data mahasiswa tidak ditemukan."); return; }

        Prodi prodiLama = CariProdi(m.KodeProdi);
        Dosen dosenLama = CariDosen(m.NidnPembimbingAkademik);
        Console.WriteLine($"Data lama: {m.Nim} | {m.NamaLengkap} | {TeksJenisKelamin(m.JenisKelamin)}");
        Console.WriteLine($"Prodi: {prodiLama?.NamaProdi} | Pembimbing: {dosenLama?.NamaLengkap}");

        m.NamaLengkap = BacaNama("Nama baru (5-50 karakter): ");
        m.JenisKelamin = BacaJenisKelamin();
        Prodi prodi = PilihProdi();
        if (prodi == null) return;
        Dosen dosen = PilihDosen();
        if (dosen == null) return;

        m.KodeProdi = prodi.KodeProdi;
        m.NidnPembimbingAkademik = dosen.Nidn;
        Pesan("Data mahasiswa berhasil diperbarui. NIM tetap.");
    }

    private static void HapusMahasiswa()
    {
        Console.Clear();
        Judul("HAPUS MAHASISWA");
        Mahasiswa m = CariMahasiswa(BacaTeks("Masukkan NIM: "));
        if (m == null) { Pesan("Data mahasiswa tidak ditemukan."); return; }

        Prodi p = CariProdi(m.KodeProdi);
        Dosen d = CariDosen(m.NidnPembimbingAkademik);
        Console.WriteLine($"Mahasiswa ditemukan: {m.NamaLengkap}");
        Console.WriteLine($"Prodi: {p?.NamaProdi} | Pembimbing: {d?.NamaLengkap}");
        if (KonfirmasiHapus())
        {
            DaftarMahasiswa.Remove(m);
            Pesan("Data mahasiswa berhasil dihapus.");
        }
        else Pesan("Penghapusan dibatalkan.");
    }
}
