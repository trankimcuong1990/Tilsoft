using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialOrderNorm.DTO
{
    public class FactoryMaterialOrderNormDetail
    {
        public int? FactoryMaterialOrderNormDetailID { get; set; }
        public int? FactoryMaterialOrderNormID { get; set; }
        public int? FactoryMaterialID { get; set; }
        public int? UnitID { get; set; }
        public decimal? NormValue { get; set; }
        public int? FactoryGoodsProcedureID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
    }
}
