using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryFinishedProductReceipt.DTO
{
    public class FactoryFinishedProductReceiptDetail
    {
        public int? FactoryFinishedProductReceiptDetailID { get; set; }
        public int? FactoryFinishedProductReceiptID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Remark { get; set; }
        public int? FromGoodsProcedureID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryFinishedProductOrderNormID { get; set; }
        public int? ProductID { get; set; }
        public int? FactoryMaterialNormID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public int? StockQnt { get; set; }
    }
}
