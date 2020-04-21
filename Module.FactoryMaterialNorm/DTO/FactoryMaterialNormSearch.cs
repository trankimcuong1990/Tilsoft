using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialNorm.DTO
{
    public class FactoryMaterialNormSearch
    {
        public int FactoryMaterialNormID { get; set; }
        public string FactoryMaterialNormUD { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        
    }
}
