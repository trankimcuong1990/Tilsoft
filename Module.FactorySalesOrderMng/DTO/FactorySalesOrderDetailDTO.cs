using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySalesOrderMng.DTO
{
   public class FactorySalesOrderDetailDTO
    {
        public int FactorySaleOrderDetailID { get; set; }
        public int? FactorySaleOrderID { get; set; }
        public int? ProductionItemID { get; set; }        
        public decimal? Quantity { get; set; }       
        public decimal? UnitPrice { get; set; }       
        public decimal? DiscountPercent { get; set; }        
        public decimal? VATPercent { get; set; }
        public int? FactoryWarehouseID { get; set; }        
        public string Remark { get; set; }
        public string ProductionItemNM { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string UnitNM { get; set; }
        public string UnitID { get; set; }
    }
}
