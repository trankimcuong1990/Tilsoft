﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceOtherMng.DTO
{
    public class FactoryInvoice
    {
        public int FactoryInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? SupplierID { get; set; }
        public int? BookingID { get; set; }
        public decimal? SubTotalAmount { get; set; }
        public decimal? DeductedAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ScanFile { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string SupplierUD { get; set; }
        public string SupplierNM { get; set; }

        public List<FactoryInvoiceExtra> FactoryInvoiceExtras { get; set; }

        public string ScanFile_NewFile { get; set; }
        public bool ScanFile_HasChange { get; set; }
    }
}
