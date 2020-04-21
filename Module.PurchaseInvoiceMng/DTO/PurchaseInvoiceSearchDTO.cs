using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class PurchaseInvoiceSearchDTO
    {
        public int PurchaseInvoiceID { get; set; }
        public string PurchaseInvoiceUD { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<int> PurchaseInvoiceTypeID { get; set; }
        public string PurchaseInvoiceDate { get; set; }
        public string PostingDate { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public Nullable<int> PurchaseInvoiceStatusID { get; set; }
        public string AttachedFile { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string FriendlyName { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string PurchaseInvoiceStatusNM { get; set; }
        public string PurchaseInvoiceTypeNM { get; set; }
        public Nullable<int> SetStatusBy { get; set; }
        public string SetStatusDate { get; set; }
        public string StatusName { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public decimal? Amount { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
}
