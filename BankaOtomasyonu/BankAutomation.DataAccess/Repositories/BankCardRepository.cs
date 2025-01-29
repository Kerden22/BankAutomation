using System;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class BankCardRepository
    {
        private readonly string _connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public void AddBankCard(BankCard bankCard)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO BankaKartı (MusteriNo, KartSahibiAdi, Skt, CVV, KartTur, KartNumarası) " +
                            "VALUES (@MusteriNo, @KartSahibiAdi, @Skt, @CVV, @KartTur, @KartNumarası)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MusteriNo", bankCard.MusteriNo);
                    command.Parameters.AddWithValue("@KartSahibiAdi", bankCard.KartSahibiAdi);
                    command.Parameters.AddWithValue("@Skt", bankCard.Skt);
                    command.Parameters.AddWithValue("@CVV", bankCard.CVV);
                    command.Parameters.AddWithValue("@KartTur", bankCard.KartTur);
                    command.Parameters.AddWithValue("@KartNumarası", bankCard.KartNumarası);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
