using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.AVFOutwardInvoiceMng.DTO
{
    public class AVFOutwardInvoice
    {
        public int? AVFOutwardInvoiceID { get; set; }
        public string SerialNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Season { get; set; }
        public string CreditorNM { get; set; }
        public string CreditorTaxCode { get; set; }
        public string CustomsAccount { get; set; }
        public string POD { get; set; }
        public string POA { get; set; }
        public string TaxRate { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        //
        // concurrency
        //
        public string ConcurrencyFlag_String { get; set; }
        public List<AVFOutwardInvoiceDetail> AVFOutwardInvoiceDetails { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatorName2 { get; set; }
    }
}
