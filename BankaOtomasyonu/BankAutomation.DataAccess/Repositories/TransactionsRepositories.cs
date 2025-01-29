using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class TransactionRepositories
    {
        private readonly string _connectionString =
             "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        // Tüm işlemleri getiren metot
        public List<Transactions> GetAllTransactions()
        {
            var transactions = new List<Transactions>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Islemler";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var transaction = new Transactions
                        {
                            IslemNo = Convert.ToInt32(reader["islemno"]),
                            MusteriNo = Convert.ToInt32(reader["musterino"]),
                            islemtarihi = Convert.ToDateTime(reader["islemtarihi"]),
                            IslemTur = reader["islemtur"].ToString(),
                            Tutar = Convert.ToDecimal(reader["tutar"])
                        };
                        transactions.Add(transaction);
                    }
                }
            }

            return transactions;
        }

        // Giriş yapan müşterinin işlemlerini getiren metot
        public List<Transactions> GetTransactionsByCustomerId(int customerId)
        {
            var transactions = new List<Transactions>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT islemno, musterino, islemtarihi, islemtur, tutar FROM Islemler WHERE musterino = @CustomerId ORDER BY islemtarihi DESC";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        transactions.Add(new Transactions
                        {
                            IslemNo = Convert.ToInt32(reader["islemno"]),
                            MusteriNo = Convert.ToInt32(reader["musterino"]),
                            islemtarihi = Convert.ToDateTime(reader["islemtarihi"]),
                            IslemTur = reader["islemtur"].ToString(),
                            Tutar = Convert.ToDecimal(reader["tutar"])
                        });
                    }
                }
            }

            return transactions;
        }


        // Havale işlemini gerçekleştiren metot
        public void ExecuteHavale(int gonderenHesapNo, int alanHesapNo, decimal tutar)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("Havale", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@GonderenHesapNo", gonderenHesapNo);
                    command.Parameters.AddWithValue("@AlanHesapNo", alanHesapNo);
                    command.Parameters.AddWithValue("@Tutar", tutar);

                    // Stored procedure'ü çalıştır
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
