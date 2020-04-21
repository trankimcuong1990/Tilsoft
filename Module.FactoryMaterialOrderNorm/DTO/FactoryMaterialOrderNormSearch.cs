using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialOrderNorm.DTO
{
    public class FactoryMaterialOrderNormSearch
    {
        public int FactoryMaterialOrderNormID { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
    }
}
