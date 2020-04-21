using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng.DTO
{
    public class FactoryOrderItemSearchResult
    {
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public string ItemType { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public int? OrderQnt { get; set; }
        public int? FactoryID { get; set; }
        public int? ClientID { get; set; }
        public string Season { get; set; }
        public bool IsSelected { get; set; }
    }
}
