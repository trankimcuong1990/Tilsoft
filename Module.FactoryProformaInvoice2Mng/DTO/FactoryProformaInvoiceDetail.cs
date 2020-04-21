using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng.DTO
{
    public class FactoryProformaInvoiceDetail
    {
        public int FactoryProformaInvoiceDetailID { get; set; }
        public int? FactoryProformaInvoiceID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryOrderSparepartDetailID { get; set; }
        public decimal? UnitPrice1 { get; set; }
        public decimal? UnitPrice2 { get; set; }
        public decimal? UnitPrice3 { get; set; }
        public decimal? UnitPrice4 { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string Remark { get; set; }
        public string FactoryOrderUD { get; set; }
        public string LDS { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public decimal? PurchasingPrice { get; set; }
        public string UpdatorName { get; set; }
        public string ItemType { get; set; }
        public int? OrderQnt { get; set; }
    }
}
