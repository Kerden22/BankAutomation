using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString =
               "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public bool ValidateEmployee(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM Calisanlar WHERE calisanadi = @username AND sifre = @password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Eğer kullanıcı adı ve şifre eşleşiyorsa true döner
                }
            }
        }
    }
}
