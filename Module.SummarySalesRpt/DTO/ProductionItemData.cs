using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummarySalesRpt.DTO
{
    public class ProductionItemData
    {
        public int FactorySaleOrderDetailID { get; set; }
        public int? FactorySaleOrderID { get; set; }
        public string FactorySaleOrderUD { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public decimal? Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? VATPercent { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int? CompanyID { get; set; }
        public string DocumentDate { get; set; }
        public string Remark { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? TotalQtyDeliveryNote { get; set; }
    }
}
