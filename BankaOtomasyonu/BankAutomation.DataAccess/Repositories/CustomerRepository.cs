using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class CustomerRepository
    {
        private readonly string _connectionString = "" +
                "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Musteriler", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        Musterino = Convert.ToInt32(reader["musterino"]),
                        isim = reader["isim"]?.ToString(),
                        soyisim = reader["soyisim"]?.ToString(),
                        dogumtarihi = Convert.ToDateTime(reader["dogumtarihi"]),
                        cinsiyet = reader["cinsiyet"]?.ToString(),
                        telno = reader["telno"]?.ToString(),
                        sifre = reader["sifre"]?.ToString(),
                    };
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public void AddCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Musteriler (isim, soyisim, dogumtarihi, cinsiyet, telno, sifre) " +
                            "VALUES (@isim, @soyisim, @dogumtarihi, @cinsiyet, @telno, @sifre)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@isim", customer.isim);
                    command.Parameters.AddWithValue("@soyisim", customer.soyisim);
                    command.Parameters.AddWithValue("@dogumtarihi", customer.dogumtarihi);
                    command.Parameters.AddWithValue("@cinsiyet", customer.cinsiyet);
                    command.Parameters.AddWithValue("@telno", customer.telno);
                    command.Parameters.AddWithValue("@sifre", customer.sifre);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
