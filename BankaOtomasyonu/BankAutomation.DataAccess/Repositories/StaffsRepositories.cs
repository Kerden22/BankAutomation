using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class StaffsRepositories
    {
        private readonly string _connectionString =
                "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<Staffs> GetAllStaffs()
        {
            var staffs = new List<Staffs>(); // Çalışanlar listesini oluşturuyoruz.

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open(); // Veritabanı bağlantısını açıyoruz.

                var query = "SELECT * FROM Personeller"; // Personeller tablosundaki tüm verileri alıyoruz.
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader(); // Sorguyu çalıştırıp sonucu okuyucuya aktarıyoruz.
                    while (reader.Read())
                    {
                        // Okunan her satırı bir Staffs nesnesine çevirip listeye ekliyoruz.
                        staffs.Add(new Staffs
                        {
                            PersonelNo = (int)reader["PersonelNo"],
                            PersonelAd = reader["PersonelAd"].ToString(),
                            personelsoyad = reader["personelsoyad"].ToString(),
                            UnvanNo = (int)reader["UnvanNo"]
                        });
                    }
                }
            }

            return staffs; // Çalışanlar listesini geri döndürüyoruz.
        }

        public Staffs GetStaffById(int id)
        {
            Staffs staff = null; // Tek bir çalışan için nesne oluşturuyoruz.

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open(); // Veritabanı bağlantısını açıyoruz.

                var query = "SELECT * FROM Personeller WHERE PersonelNo = @PersonelNo";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonelNo", id); // Parametreyi ekliyoruz.

                    var reader = command.ExecuteReader(); // Sorguyu çalıştırıp sonucu okuyucuya aktarıyoruz.
                    if (reader.Read())
                    {
                        // Okunan satırı Staffs nesnesine çeviriyoruz.
                        staff = new Staffs
                        {
                            PersonelNo = (int)reader["PersonelNo"],
                            PersonelAd = reader["PersonelAd"].ToString(),
                            personelsoyad = reader["personelsoyad"].ToString(),
                            UnvanNo = (int)reader["UnvanNo"]
                        };
                    }
                }
            }

            return staff; // Tek bir çalışan nesnesini geri döndürüyoruz.
        }
    }
}
