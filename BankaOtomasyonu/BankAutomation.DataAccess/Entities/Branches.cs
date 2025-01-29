using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class Branches
    {
        public int SubeNo { get; set; } // Primary Key
        public string SubeAdi { get; set; }
        public string SubeTel { get; set; }
    }
}
