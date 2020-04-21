using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItemGroup.DTO
{
    public class ProductionItemGroupSearchDTO
    {
        public int? ProductionItemGroupID { get; set; }
        public string ProductionItemGroupUD { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
    }
}
