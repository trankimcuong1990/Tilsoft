using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class MaterialPriceProductItemSeach
    {
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public int UnitID { get; set; }
        public string UnitNM { get; set; }
        public List<Unit> Units { get; set; }
    }
}
