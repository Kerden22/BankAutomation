using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class Transactions
    {
        public int IslemNo { get; set; } // Primary Key
        public int MusteriNo { get; set; } // Foreign Key
        public DateTime islemtarihi { get; set; }
        public string IslemTur { get; set; }
        public decimal Tutar { get; set; }
    }
}
