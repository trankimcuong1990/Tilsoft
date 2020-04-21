using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.FactoryMaterial.DTO
{
    public class FactoryMaterial
    {
        public int? FactoryMaterialID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public string FactoryMaterialEnglishName { get; set; }
        public int? FactoryMaterialGroupID { get; set; }
        public int? FactoryMaterialTypeID { get; set; }
        public int? FactoryMaterialColorID { get; set; }
        public int? UnitID { get; set; }
        public List<FactoryMaterialImage> FactoryMaterialImages { get; set; }
    }
}