using BankAutomation.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAutomation.DataAccess.Repositories
{
    public class DeletedCustomersRepositories
    {
        private readonly string _connectionString =
              "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<DeletedCustomers> GetAllDeletedCustomers()
        {
            var deletedCustomers = new List<DeletedCustomers>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open(); // Veritabanı bağlantısını açıyoruz.
                var query = "SELECT * FROM dbo.SilinenMusteriler"; // SilinenMusteriler tablosundan tüm verileri alıyoruz.
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader()) // Sorguyu çalıştırıp sonuçları okuyucuya alıyoruz.
                    {
                        while (reader.Read())
                        {
                            var deletedCustomer = new DeletedCustomers
                            {
                                MusteriNo = Convert.ToInt32(reader["musterino"]),
                                Isim = reader["isim"].ToString(),
                                Soyisim = reader["soyisim"].ToString(),
                                DogumTarihi = Convert.ToDateTime(reader["dogumtarihi"]),
                                Cinsiyet = reader["cinsiyet"].ToString(),
                                TelNo = reader["telno"].ToString(),
                                SilinmeTarihi = Convert.ToDateTime(reader["silinme_tarihi"])
                            };

                            deletedCustomers.Add(deletedCustomer); // Her bir müşteri kaydını listeye ekliyoruz.
                        }
                    }
                }
            }

            return deletedCustomers; // Silinen müşterilerin listesini döndürüyoruz.
        }
    }
}
