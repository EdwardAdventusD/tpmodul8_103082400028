using System;
using tpmodul8_103082400028;

namespace tpmodul8_103082400028
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Agar karakter khusus (seperti 'é' pada 'ditolak') bisa tampil

            // 1. Membaca atau membuat file konfigurasi
            CovidConfig config = new CovidConfig();

            // 2. Menampilkan pesan dan meminta input dari user
            Console.WriteLine($"Berapa suhu badan anda saat ini? Dalam nilai {config.satuan_suhu}");
            double suhu = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam?");
            int hariDemam = Convert.ToInt32(Console.ReadLine());

            // 3. Logika pengecekan kondisi
            bool suhuValid = false;
            if (config.satuan_suhu.ToLower() == "celcius")
            {
                // Cek range untuk celcius: 36.5 - 37.5
                if (suhu >= 36.5 && suhu <= 37.5)
                    suhuValid = true;
            }
            else if (config.satuan_suhu.ToLower() == "fahrenheit")
            {
                // Cek range untuk fahrenheit: 97.7 - 99.5
                if (suhu >= 97.7 && suhu <= 99.5)
                    suhuValid = true;
            }

            bool hariValid = (hariDemam < config.batas_hari_deman);

            // 4. Menampilkan hasil sesuai kondisi
            if (suhuValid && hariValid)
            {
                Console.WriteLine(config.pesan_diterima);
            }
            else
            {
                Console.WriteLine(config.pesan_ditolak);
            }

            // 5. Soal 3G: Memanggil method UbahSatuan
            Console.WriteLine("\n--- Mengganti Satuan Suhu ---");
            config.UbahSatuan();

            // Optional: Tunjukkan konfigurasi baru setelah diubah
            Console.WriteLine("Konfigurasi baru setelah diubah:");
            config.SaveConfig(); // Tidak wajib dipanggil, karena sudah dipanggil di method UbahSatuan
            Console.WriteLine("Tekan sembarang tombol untuk keluar...");
            Console.ReadKey();
        }
    }
}