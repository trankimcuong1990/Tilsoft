using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Module.Support.DTO
{
    public class FactoryMaterials {
        public List<FactoryMaterial> Data { get; set; }
        public int TotalRows { get; set; }
    }
    
    public class FactoryMaterial
    {
        public int? FactoryMaterialID { get; set; }
        public string FactoryMaterialUD { get; set; }
        public string FactoryMaterialNM { get; set; }
        public string FactoryMaterialEnglishName { get; set; }
        public string FactoryMaterialGroupNM { get; set; }
        public string FactoryMaterialTypeNM { get; set; }
        public string FactoryMaterialColorNM { get; set; }
        public string UnitNM { get; set; }
        public int? UnitID { get; set; }
        public List<FactoryMaterialImage> FactoryMaterialImages { get; set; }
    }
}