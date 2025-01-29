using BankAutomation.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Repositories
{
    public class AccountsRepositories
    {
        private readonly string _connectionString =
                "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<Accounts> GetAllAccounts()
        {
            var accounts = new List<Accounts>(); // Accounts listesi oluşturuyoruz.

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open(); // Veritabanı bağlantısını açıyoruz.
                var query = "SELECT * FROM Hesaplar"; // Hesaplar tablosundaki tüm kayıtları alıyoruz.
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = new Accounts
                            {
                                HesapNo = Convert.ToInt32(reader["hesapno"]),
                                MusteriNo = Convert.ToInt32(reader["musterino"]),
                                Bakiye = Convert.ToDecimal(reader["bakiye"]),
                                HesapTur = reader["hesaptur"].ToString(),
                            };

                            accounts.Add(account); // Listeye hesap ekliyoruz.
                        }
                    }
                }
            }

            return accounts; // Hesap listesini döndürüyoruz.
        }
        public bool IsAccountBelongsToCustomer(int hesapNo, int musteriNo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM Hesaplar WHERE HesapNo = @HesapNo AND MusteriNo = @MusteriNo";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HesapNo", hesapNo);
                    command.Parameters.AddWithValue("@MusteriNo", musteriNo);

                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Hesap, müşteriyle eşleşiyorsa true döner
                }
            }
        }

    }
}
