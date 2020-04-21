﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2BalanceMng.DTO
{
    public class Increasing
    {
        public string Season { get; set; }
        public string ReceiptNo { get; set; }
        public string CreditNoteNo { get; set; }
        public string IssuedDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
