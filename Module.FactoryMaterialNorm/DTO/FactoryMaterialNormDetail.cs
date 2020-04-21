using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialNorm.DTO
{
    public class FactoryMaterialNormDetail
    {
        public int FactoryMaterialNormDetailID { get; set; }
        public int? FactoryMaterialNormID { get; set; }
        public int? FactoryMaterialID { get; set; }
        public int? UnitID { get; set; }
        public decimal? NormValue { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
    }
}
