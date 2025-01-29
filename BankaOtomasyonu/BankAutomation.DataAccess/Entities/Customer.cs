using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class Customer
    {
        public int Musterino { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public DateTime dogumtarihi { get; set; }
        public string cinsiyet { get; set; }
        public string telno { get; set; }
        public string sifre { get; set; }
    }
}
