using Modul08_KPL;

class Program
{
    static void Main(string[] args)
    {
        // Memuat konfigurasi dari file JSON
        CovidConfig config = CovidConfig.LoadConfig("covid_config.json");

        // Menanyakan suhu badan pengguna berdasarkan satuan suhu yang ada di konfigurasi
        Console.Write($"Berapa suhu badan anda saat ini? (dalam {config.satuan_suhu}): ");
        double suhu = Convert.ToDouble(Console.ReadLine());  // Mengambil input suhu dari pengguna

        // Menanyakan berapa hari yang lalu pengguna merasa demam
        Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
        int hari = Convert.ToInt32(Console.ReadLine());  // Mengambil input jumlah hari dari pengguna

        // Mendefinisikan batas suhu normal berdasarkan satuan suhu (Celsius atau Fahrenheit)
        double batasMin, batasMax;
        if (config.satuan_suhu.ToLower() == "celcius")  // Jika satuan suhu adalah Celsius
        {
            batasMin = 36.5;  // Suhu tubuh minimal normal dalam Celsius
            batasMax = 37.5;  // Suhu tubuh maksimal normal dalam Celsius
        }
        else  // Jika satuan suhu adalah Fahrenheit
        {
            batasMin = 97.7;  // Suhu tubuh minimal normal dalam Fahrenheit
            batasMax = 99.5;  // Suhu tubuh maksimal normal dalam Fahrenheit
        }

        // Mengecek apakah suhu dan jumlah hari demam sesuai dengan batas yang ditentukan
        if (suhu >= batasMin && suhu <= batasMax && hari < Convert.ToInt32(config.batas_hari_deman))
        {
            Console.WriteLine(config.pesan_diterima);  // Menampilkan pesan diterima
        }
        else
        {
            Console.WriteLine(config.pesan_ditolak);  // Menampilkan pesan ditolak
        }

        // Mengubah satuan suhu setelah pengecekan
        config.UbahSatuan();
        Console.WriteLine($"Satuan suhu sekarang diubah menjadi: {config.satuan_suhu}");

        // Mengonversi suhu sesuai dengan satuan yang baru
        double suhuSetelahKonversi;
        if (config.satuan_suhu == "fahrenheit")  // Jika satuan suhu baru adalah Fahrenheit
        {
            suhuSetelahKonversi = (suhu * 9 / 5) + 32;  // Konversi dari Celsius ke Fahrenheit
        }
        else  // Jika satuan suhu baru adalah Celsius
        {
            suhuSetelahKonversi = (suhu - 32) * 5 / 9;  // Konversi dari Fahrenheit ke Celsius
        }

        // Menampilkan suhu setelah konversi ke satuan yang baru
        Console.WriteLine($"Suhu badan anda setelah dikonversi: {suhuSetelahKonversi:F1}° {config.satuan_suhu}");
    }
}
