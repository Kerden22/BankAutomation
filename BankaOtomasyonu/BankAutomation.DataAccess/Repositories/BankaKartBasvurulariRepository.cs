using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankAutomation.DataAccess.Entities;

namespace BankAutomation.DataAccess.Repositories
{
    public class BankaKartBasvurulariRepository
    {
        private readonly string _connectionString =
                "Data Source=.\\SQLEXPRESS;Initial Catalog=Banka;Integrated Security=True;";

        // Yeni başvuru ekle
        public void AddBankaKartBasvurusu(BankaKartBasvurulari basvuru)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO BankaKartBasvurulari (MusteriNo, AdSoyad, DogumTarihi, Cinsiyet, TelefonNo, Adres) " +
                            "VALUES (@MusteriNo, @AdSoyad, @DogumTarihi, @Cinsiyet, @TelefonNo, @Adres)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MusteriNo", basvuru.MusteriNo);
                    command.Parameters.AddWithValue("@AdSoyad", basvuru.AdSoyad);
                    command.Parameters.AddWithValue("@DogumTarihi", basvuru.DogumTarihi);
                    command.Parameters.AddWithValue("@Cinsiyet", basvuru.Cinsiyet);
                    command.Parameters.AddWithValue("@TelefonNo", basvuru.TelefonNo);
                    command.Parameters.AddWithValue("@Adres", basvuru.Adres);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Tüm başvuruları getir
        public List<BankaKartBasvurulari> GetAllBasvurular()
        {
            var basvurular = new List<BankaKartBasvurulari>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM BankaKartBasvurulari";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        basvurular.Add(new BankaKartBasvurulari
                        {
                            BasvuruId = Convert.ToInt32(reader["BasvuruId"]),
                            MusteriNo = Convert.ToInt32(reader["MusteriNo"]),
                            AdSoyad = reader["AdSoyad"].ToString(),
                            DogumTarihi = Convert.ToDateTime(reader["DogumTarihi"]),
                            Cinsiyet = reader["Cinsiyet"].ToString(),
                            TelefonNo = reader["TelefonNo"].ToString(),
                            Adres = reader["Adres"].ToString(),
                            BasvuruTarihi = Convert.ToDateTime(reader["BasvuruTarihi"])
                        });
                    }
                }
            }

            return basvurular;
        }

        // Başvuru sil (onaylandıktan sonra)
        public void DeleteBasvuru(int basvuruId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM BankaKartBasvurulari WHERE BasvuruId = @BasvuruId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BasvuruId", basvuruId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
