using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng.DTO
{
    public class SpecificationPackingDTO
    {
        public int ProductSpecificationPackingID { get; set; }
        public int? ProductSpecificationID { get; set; }
        public int? PackingSpecificationID { get; set; }
        public bool? IsSelected { get; set; }
        public string PackingSpecificationNM { get; set; }
    }
}
