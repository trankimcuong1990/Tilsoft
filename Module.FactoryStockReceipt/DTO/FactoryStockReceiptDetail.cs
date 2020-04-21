using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryStockReceipt.DTO
{
    public class FactoryStockReceiptDetail
    {
        public int? FactoryStockReceiptDetailID { get; set; }
        public int? FactoryStockReceiptID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Remark { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? ProducedQnt { get; set; }
        public int? NotPackedQnt { get; set; }
        public int? PackedQnt { get; set; }


        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? TotalStockQnt { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public int? TotalPackedQnt { get; set; }
        public int? TotalShipped { get; set; }

    }
}