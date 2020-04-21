using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryOrderNorm.DTO
{
    public class FactoryMaterialOrderNorm
    {
        public int? FactoryMaterialOrderNormID { get; set; }
        public int? FactoryFinishedProductOrderNormID { get; set; }
        public int? FactoryMaterialID { get; set; }
        public decimal? NormValue { get; set; }
        public int? UnitID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public string UnitNM { get; set; }
        public bool? IsEditing { get; set; }
    }
}