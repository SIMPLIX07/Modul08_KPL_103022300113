using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Modul08_KPL
{
    // Kelas untuk menyimpan dan mengelola pengaturan konfigurasi terkait Covid
    class CovidConfig
    {
        // Properti untuk konfigurasi dengan nilai default
        public string satuan_suhu { get; set; } = "celcius";  // Satuan suhu (Celcius sebagai default)
        public int batas_hari_deman { get; set; } = 14;        // Batas maksimal hari demam (14 hari sebagai default)
        public string pesan_ditolak { get; set; } = "Anda tidak diperbolehkan masuk ke dalam gedung ini";  // Pesan untuk penolakan
        public string pesan_diterima { get; set; } = "Anda dipersilahkan untuk masuk ke dalam gedung ini";  // Pesan untuk penerimaan

        // Metode untuk memuat dan mendeserialisasi konfigurasi dari file JSON
        public static CovidConfig LoadConfig(string path)
        {
            if (File.Exists(path)) // Mengecek apakah file konfigurasi ada
            {
                string json = File.ReadAllText(path);  // Membaca isi file sebagai string
                var config = JsonSerializer.Deserialize<CovidConfig>(json);  // Mendeserialisasi string JSON menjadi objek CovidConfig

                // Jika ada properti yang memiliki nilai placeholder (misalnya CONFIG1), ganti dengan nilai default
                if (config.satuan_suhu == "CONFIG1") config.satuan_suhu = "celcius";
                if (config.batas_hari_deman == 0 || config.batas_hari_deman.ToString() == "CONFIG2") config.batas_hari_deman = 14;
                if (config.pesan_ditolak == "CONFIG3") config.pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
                if (config.pesan_diterima == "CONFIG4") config.pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

                return config;  // Mengembalikan objek konfigurasi yang telah dideserialisasi
            }

            // Jika file tidak ada, kembalikan objek CovidConfig baru dengan nilai default
            return new CovidConfig();
        }

        // Metode untuk mengubah satuan suhu antara Celcius dan Fahrenheit
        public void UbahSatuan()
        {
            if (satuan_suhu == "celcius")  // Jika satuan suhu saat ini adalah Celcius
                satuan_suhu = "fahrenheit";  // Ubah ke Fahrenheit
            else
                satuan_suhu = "celcius";  // Jika tidak, ubah ke Celcius
        }
    }
}
