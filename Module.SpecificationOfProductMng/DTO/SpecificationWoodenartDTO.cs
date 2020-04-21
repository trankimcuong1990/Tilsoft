using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng.DTO
{
    public class SpecificationWoodenartDTO
    {
        public int ProductSpecificationWoodenPartID { get; set; }
        public int? ProductSpecificationID { get; set; }
        public int? RowIndex { get; set; }
        public string DimensionL { get; set; }
        public string DimensionW { get; set; }
        public string DimensionH { get; set; }
        public string Weight { get; set; }
    }
}
