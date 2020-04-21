using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemType.DTO
{
    public class ProductionItemTypeSearchDTO
    {
        public int? ProductionItemTypeID { get; set; }
        public string ProductionItemTypeUD { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public int? ProductionItemMaterialTypeID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
    }
}
