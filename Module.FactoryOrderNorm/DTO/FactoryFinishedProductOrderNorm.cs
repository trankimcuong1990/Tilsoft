using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryOrderNorm.DTO
{
    public class FactoryFinishedProductOrderNorm
    {
        public int? FactoryFinishedProductOrderNormID { get; set; }
        public int? FactoryOrderNormID { get; set; }
        public int? FactoryFinishedProductID { get; set; }
        public decimal? NormValue { get; set; }
        public int? UnitID { get; set; }
        public int? ParentNormID { get; set; }
        public int? MaterialGroupTypeID { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }

        public bool? IsEditing { get; set; }
        public List<FactoryMaterialOrderNorm> FactoryMaterialOrderNorms { get; set; }
        public List<FactoryFinishedProductOrderNorm> FactoryFinishedProductOrderNorms { get; set; }
        public List<FactoryFinishedProductOrderNormFactoryGoodsProcedure> FactoryFinishedProductOrderNormFactoryGoodsProcedures { get; set; }
    }
}