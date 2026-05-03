using Newtonsoft.Json;
using System.IO;

namespace tpmodul8_103082400028
{
    public class CovidConfig
    {
        // Properti sesuai dengan key di file json
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        // Nama file konfigurasi
        private const string fileName = "covid_config.json";

        // Konstruktor untuk membaca file konfigurasi saat objek dibuat
        public CovidConfig()
        {
            // Jika file konfigurasi belum ada, buat dengan nilai default
            if (!File.Exists(fileName))
            {
                SetDefault();
                SaveConfig();
            }
            else
            {
                LoadConfig();
            }
        }

        // Method untuk membaca konfigurasi dari file
        private void LoadConfig()
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                // Deserialisasi JSON menjadi objek CovidConfig
                var config = JsonConvert.DeserializeObject<CovidConfig>(jsonString);

                if (config != null)
                {
                    this.satuan_suhu = config.satuan_suhu;
                    this.batas_hari_deman = config.batas_hari_deman;
                    this.pesan_ditolak = config.pesan_ditolak;
                    this.pesan_diterima = config.pesan_diterima;
                }
                else
                {
                    // Jika gagal deserialisasi, gunakan default
                    SetDefault();
                }
            }
            catch
            {
                // Jika ada error baca file, gunakan default
                SetDefault();
            }
        }

        // Method untuk menyimpan konfigurasi ke file
        public void SaveConfig()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }

        // Method untuk mengisi nilai default
        private void SetDefault()
        {
            satuan_suhu = "celcius";
            batas_hari_deman = 14;
            pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
            pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
        }

        // Method untuk mengganti satuan suhu (Soal 3F)
        public void UbahSatuan()
        {
            if (satuan_suhu.ToLower() == "celcius")
                satuan_suhu = "fahrenheit";
            else if (satuan_suhu.ToLower() == "fahrenheit")
                satuan_suhu = "celcius";

            // Setelah diubah, simpan perubahan ke file
            SaveConfig();

            Console.WriteLine($"Satuan suhu telah diubah menjadi '{satuan_suhu}'.");
        }
    }
}