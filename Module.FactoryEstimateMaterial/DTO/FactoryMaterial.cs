using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryEstimateMaterial.DTO
{
    public class FactoryMaterial
    {
        
        public int? FactoryMaterialID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public decimal? Quantity { get; set; }
        public int? UnitID { get; set; }
        public string UnitNM { get; set; }
        public decimal? TotalStockQnt { get; set; }


        public bool? IsSelected { get; set; }
        public List<DTO.FactoryOrderDetail> FactoryOrderDetails { get; set; }
        public int? FactoryAreaID { get; set; }
    }
}
