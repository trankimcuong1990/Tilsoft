﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountReceivableRpt.DTO
{
    public class TotalLiabilities
    {
        public int? FactoryRawMaterialID { get; set; }
        public decimal? TotalPurchasingInvoice { get; set; }
        public decimal? TotalReceiptNoteInvoice { get; set; }
        public decimal? BeginningHas { get; set; }
        public decimal? BeginningDebt { get; set; }
        public decimal? EndingBalanceHas { get; set; }
        public decimal? EndingBalanceDebt { get; set; }
        public decimal? EndingBalanceUSDHas { get; set; }
        public decimal? EndingBalanceUSDDebt { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
    }
}
