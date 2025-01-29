using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class TitleRepositories
    {
        private readonly string _connectionString =
             "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<Title> GetAllTitles()
        {
            var titles = new List<Title>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Unvan";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var title = new Title
                        {
                            UnvanNo = Convert.ToInt32(reader["unvanno"]),
                            UnvanAdi = reader["unvanadi"].ToString()
                        };
                        titles.Add(title);
                    }
                }
            }

            return titles;
        }
    }
}
