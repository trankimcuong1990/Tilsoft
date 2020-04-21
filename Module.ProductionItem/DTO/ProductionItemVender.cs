using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class ProductionItemVender
    {
        public int? ProductionItemVenderID { get; set; }
        public int? ProductItemID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public bool? IsDefault { get; set; }
        public string Remark { get; set; }
    }
}
