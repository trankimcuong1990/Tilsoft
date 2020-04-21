using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryNorm.DTO
{
    public class FactoryMaterialNorm
    {
        public int FactoryMaterialNormID { get; set; }
        public int? FactoryFinishedProductNormID { get; set; }
        public int? FactoryMaterialID { get; set; }
        public decimal? NormValue { get; set; }
        public int? UnitID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public string UnitNM { get; set; }
        public bool? IsEditing { get; set; }
    }
}
