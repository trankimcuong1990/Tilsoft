using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class ProductionItemWarehouse
    {
        public int? ProductionItemWarehouseID { get; set; }
        public int? ProductItemID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public string FactoryWarehouseNM { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public bool? IsDefault { get; set; }
        public int? InStock { get; set; }
        public string Description { get; set; }
        public int? Committed { get; set; }
        public int? Ordered { get; set; }
        public int? Available { get; set; }

        // Add new properties: BranchID.
        public int? BranchID { get; set; }

        // Add more new properties
        public string BranchUD { get; set; }
        public string BranchNM { get; set; }
    }
}
