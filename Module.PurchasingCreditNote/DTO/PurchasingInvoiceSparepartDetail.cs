using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchasingCreditNote.DTO
{
    public class PurchasingInvoiceSparepartDetail
    {
        public int? PurchasingInvoiceSparepartDetailID { get; set; }
        public int? PurchasingInvoiceID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string LoadingPlanUD { get; set; }
        public decimal? FactoryPrice { get; set; }
    }
}
