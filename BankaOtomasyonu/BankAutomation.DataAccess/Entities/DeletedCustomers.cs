using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class DeletedCustomers
    {
        public int MusteriNo { get; set; } // Primary Key
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string TelNo { get; set; }
        public DateTime SilinmeTarihi { get; set; }
    }
}
