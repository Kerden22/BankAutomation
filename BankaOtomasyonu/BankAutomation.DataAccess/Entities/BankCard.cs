﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAutomation.DataAccess.Entities
{
    public class BankCard
    {
        public int KartNo { get; set; } // Primary Key
        public int MusteriNo { get; set; } // Foreign Key
        public string KartSahibiAdi { get; set; }
        public DateTime Skt { get; set; } // Son Kullanma Tarihi
        public string CVV { get; set; }
        public string KartTur { get; set; }
        public string KartNumarası { get; set; }
    }
}
