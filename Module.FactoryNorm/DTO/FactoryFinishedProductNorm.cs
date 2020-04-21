using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryNorm.DTO
{
    public class FactoryFinishedProductNorm
    {
        public int FactoryFinishedProductNormID { get; set; }
        public int? FactoryNormID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public decimal? NormValue { get; set; }
        public int? UnitID { get; set; }
        public int? ParentNormID { get; set; }
        public int? MaterialGroupTypeID { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }

        public bool? IsEditing { get; set; }
        public List<FactoryMaterialNorm> FactoryMaterialNorms { get; set; }
        public List<FactoryFinishedProductNorm> FactoryFinishedProductNorms { get; set; }
        public List<FactoryFinishedProductNormFactoryGoodsProcedure> FactoryFinishedProductNormFactoryGoodsProcedures { get; set; }
    }
}
