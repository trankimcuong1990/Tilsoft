using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialReceipt.DTO
{
    public class FactoryMaterialReceiptDetail
    {
        public int? FactoryMaterialReceiptDetailID { get; set; }
        public int? FactoryMaterialReceiptID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public int? FactoryFinishedProductID { get; set; }        
        public int? FactoryMaterialID { get; set; }
        public int? FactoryMaterialOrderNormID { get; set; }
        public int? FactoryAreaID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Remark { get; set; }
        
        public string FactoryUD { get; set; }
        public string LDS { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public string FactoryAreaNM { get; set; }
        public string UnitNM { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
        public decimal? TotalStockQnt { get; set; }

        public decimal? NormValue { get; set; }
        public int? OrderQnt { get; set; }

        public bool? IsFreeStock { get; set; }

        public List<FactoryMaterialReceiptDetail> FactoryMaterialReceiptDetails { get; set; }
    }
}
