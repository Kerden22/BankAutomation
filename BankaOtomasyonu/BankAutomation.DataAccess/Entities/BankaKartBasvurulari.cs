using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class BankaKartBasvurulari
    {
        public int BasvuruId { get; set; }
        public int MusteriNo { get; set; }
        public string AdSoyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string TelefonNo { get; set; }
        public string Adres { get; set; }
        public DateTime BasvuruTarihi { get; set; }
    }
}
