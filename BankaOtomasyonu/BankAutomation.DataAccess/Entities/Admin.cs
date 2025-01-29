using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class Admin
    {
        public int EmployeeId { get; set; } // calisanno
        public string FirstName { get; set; } // calisanadi
        public string LastName { get; set; } // calisansoyadi
        public int BranchId { get; set; } // subeno
        public string Password { get; set; } // sifre
    }
}
