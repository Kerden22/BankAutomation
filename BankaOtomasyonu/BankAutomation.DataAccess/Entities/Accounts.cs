using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class Accounts
    {
        public int HesapNo { get; set; } // Primary Key
        public int MusteriNo { get; set; } // Foreign Key
        public decimal Bakiye { get; set; }
        public string HesapTur { get; set; }
    }
}
