using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ModelDefaultFactory
    {
        public int ModelDefaultFactoryID { get; set; }
        public int? ModelID { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public bool? IsDefault { get; set; }

    }
}
