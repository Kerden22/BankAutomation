using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.Business
{
    public static class Session
    {
        public static int CurrentCustomerId { get; set; } // Giriş yapan müşterinin ID'sini saklar
    }
}
