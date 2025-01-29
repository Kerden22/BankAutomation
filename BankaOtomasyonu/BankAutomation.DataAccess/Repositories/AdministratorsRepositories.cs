using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class AdministratorsRepositories
    {
        private readonly string _connectionString =
                "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<Administrators> GetAllAdministrators()
        {
            var administrators = new List<Administrators>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Calisanlar";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var admin = new Administrators
                        {
                            CalisanNo = Convert.ToInt32(reader["calisanno"]),
                            CalisanAdi = reader["calisanadi"].ToString(),
                            CalisanSoyadi = reader["calisansoyadi"].ToString(),
                            SubeNo = Convert.ToInt32(reader["subeno"]),
                            Sifre = reader["sifre"].ToString()
                        };
                        administrators.Add(admin);
                    }
                }
            }

            return administrators;
        }
    }
}
