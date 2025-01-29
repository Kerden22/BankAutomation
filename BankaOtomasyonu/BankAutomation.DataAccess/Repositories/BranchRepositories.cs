using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class BranchRepositories
    {
        private readonly string _connectionString =
             "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<Branches> GetAllBranches()
        {
            var branches = new List<Branches>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Subeler";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var branch = new Branches
                        {
                            SubeNo = Convert.ToInt32(reader["subeno"]),
                            SubeAdi = reader["subeadi"].ToString(),
                            SubeTel = reader["subetel"].ToString()
                        };
                        branches.Add(branch);
                    }
                }
            }

            return branches;
        }
    }
}
