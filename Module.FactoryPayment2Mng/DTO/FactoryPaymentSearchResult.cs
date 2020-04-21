﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2Mng.DTO
{
    public class FactoryPaymentSearchResult
    {
        public int FactoryPayment2ID { get; set; }
        public string Season { get; set; }
        public string ReceiptNo { get; set; }
        public string PaymentDate { get; set; }
        public string BankReceiptNo { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }
        public decimal? DPAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public decimal? ConfirmedAmount { get; set; }
        public string ConfirmerName { get; set; }
        public string ConfirmedDate { get; set; }
        public int? SupplierID { get; set; }

        public int UpdatedBy { get; set; }
        public string UpdatorName2 { get; set; }
        public int ConfirmedBy { get; set; }
        public string ConfirmerName2 { get; set; }
    }
}
