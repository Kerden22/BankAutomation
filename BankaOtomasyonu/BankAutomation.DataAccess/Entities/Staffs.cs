using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class Staffs
    {
        public int PersonelNo { get; set; } // Primary Key
        public string PersonelAd { get; set; }
        public string personelsoyad { get; set; }
        public int UnvanNo { get; set; } // Foreign Key
    }
}

