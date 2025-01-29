using BankAutomation.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAutomation.DataAccess.Repositories
{
    public class CreditCardRepositories
    {
        private readonly string _connectionString =
               "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<CreditCard> GetAllBankCards()
        {
            var bankCards = new List<CreditCard>(); // BankCard listesi oluşturuyoruz.

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open(); // Veritabanı bağlantısını açıyoruz.
                var query = "SELECT * FROM KrediKartı"; // BankaKarti tablosundaki tüm kayıtları alıyoruz.
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bankCard = new CreditCard
                            {
                                KartNo = Convert.ToInt32(reader["kartno"]),
                                MusteriNo = Convert.ToInt32(reader["musterino"]),
                                KartSahibiAdi = reader["kartsahibiadi"].ToString(),
                                Skt = Convert.ToDateTime(reader["skt"]),
                                CVV = reader["cvv"].ToString(),
                                Limit = Convert.ToDecimal(reader["limit"]),
                                KartTur = reader["karttur"].ToString(),
                                VadeTarihi = Convert.ToDateTime(reader["vadetarihi"]),
                                KartNumarası = Convert.ToDecimal(reader["kartnumarası"])
                            };

                            bankCards.Add(bankCard); // Listeye kart ekliyoruz.
                        }
                    }
                }
            }

            return bankCards; // Kart listesini döndürüyoruz.
        }
    }
}
