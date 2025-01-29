using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class BankCardRepositories
    {
        private readonly string _connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<BankCard> GetAllBankCards()
        {
            var bankCards = new List<BankCard>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM BankaKartı";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var bankCard = new BankCard
                        {
                            KartNo = Convert.ToInt32(reader["kartno"]),
                            MusteriNo = Convert.ToInt32(reader["musterino"]),
                            KartSahibiAdi = reader["kartsahibiadi"].ToString(),
                            Skt = Convert.ToDateTime(reader["skt"]),
                            CVV = reader["cvv"].ToString(),
                            KartTur = reader["karttur"].ToString(),
                            KartNumarası = reader["Kartnumarası"].ToString() // String olarak alıyoruz
                        };
                        bankCards.Add(bankCard);
                    }
                }
            }

            return bankCards;
        }
    }
}
