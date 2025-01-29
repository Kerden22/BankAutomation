using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class CustomerRepositoryControl
    {
        private readonly string _connectionString =
               "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public bool ValidateCustomer(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open(); // Veritabanı bağlantısını aç
                var query = "SELECT COUNT(*) FROM Musteriler WHERE isim = @username AND sifre = @password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username); // Kullanıcı adı parametresi
                    command.Parameters.AddWithValue("@password", password); // Şifre parametresi

                    int count = (int)command.ExecuteScalar(); // Sorgu sonucu döner
                    return count > 0; // Eşleşme varsa true döner
                }
            }
        }

        public Customer GetCustomerByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Musteriler WHERE isim = @username"; // Kullanıcı adına göre sorgu
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                Musterino = Convert.ToInt32(reader["musterino"]),
                                isim = reader["isim"].ToString(),
                                soyisim = reader["soyisim"].ToString(),
                                dogumtarihi = Convert.ToDateTime(reader["dogumtarihi"]),
                                cinsiyet = reader["cinsiyet"].ToString(),
                                telno = reader["telno"].ToString(),
                                sifre = reader["sifre"].ToString()
                            };
                        }
                    }
                }
            }
            return null; // Kullanıcı bulunamazsa null döner
        }

    }
}
