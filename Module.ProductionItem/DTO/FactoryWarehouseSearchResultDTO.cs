using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class FactoryWarehouseSearchResultDTO
    {
        public int? ProductItemID { get; set; }

        public string FactoryWarehouseUD { get; set; }

        public string FactoryWarehouseNM { get; set; }

        public int PrimaryID { get; set; }
    }
}
