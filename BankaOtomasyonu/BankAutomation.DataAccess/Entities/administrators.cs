using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class Administrators
    {
        public int CalisanNo { get; set; } // Primary Key
        public string CalisanAdi { get; set; }
        public string CalisanSoyadi { get; set; }
        public int SubeNo { get; set; } // Foreign Key
        public string Sifre { get; set; }
    }
}
