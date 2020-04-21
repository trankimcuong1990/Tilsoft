using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFPurchasingInvoiceMng.DTO
{
    public class AVFPurchasingInvoiceSearchResult
    {
        public int? AVFPurchasingInvoiceID {get;set;}
		public string AVFPurchasingInvoiceUD {get;set;}
		public int? AVFSupplierID {get; set;}
		public string InvoiceNo {get; set;}
		public string InvoiceDate {get; set;}
        public string Season { get; set; }
        public string TaxRate { get; set; }
        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
		public string AVFSupplierNM {get; set;}
		public string AVFSupplierUD {get; set;}
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
		public string CreatorName {get; set;}
		public string UpdatorName {get; set;}
        public int UpdatorID { get; set; }
    }
}
