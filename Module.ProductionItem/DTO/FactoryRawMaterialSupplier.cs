using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionItem.DTO
{
    public class FactoryRawMaterialSupplier
    {
        public int FactoryRawMaterialSupplierID  { get; set; }
        public int? FactoryID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string FactoryRawMaterialUD { get; set; }
    }
}
