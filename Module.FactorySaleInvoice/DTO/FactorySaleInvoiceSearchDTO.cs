using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class FactorySaleInvoiceSearchDTO
    {
        public long PrimaryID { get; set; }
        public int FactorySaleInvoiceID { get; set; }
        public string InvoiceStatus { get; set; }
        public string DocCode { get; set; }
        public string InvoiceDate { get; set; }
        public string PostingDate { get; set; }
        public string InvoiceNo { get; set; }
        public string ClientNM { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string Currency { get; set; }
        public string Remark { get; set; }
        public string CreatorNM { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatorNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? Type { get; set; }
        public string Season { get; set; }
        public decimal? Amount { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
}
