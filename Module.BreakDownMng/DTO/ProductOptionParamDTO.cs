using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    internal class ProductOptionParamDTO
    {
        public int? ModelID { get; set; }
        public int? SampleProductID { get; set; }
        public int? MaterialID { get; set; }
        public int? MaterialTypeID { get; set; }
        public int? MaterialColorID { get; set; }
        public int? FrameMaterialID { get; set; }
        public int? FrameMaterialColorID { get; set; }
        public int? SubMaterialID { get; set; }
        public int? SubMaterialColorID { get; set; }
        public int? CushionTypeID { get; set; }
        public int? BackCushionID { get; set; }
        public int? SeatCushionID { get; set; }
        public int? CushionColorID { get; set; }
        public int? PackagingMethodID { get; set; }
        public int? ClientSpecialPackagingMethodID { get; set; }
        public int? FSCTypeID { get; set; }
        public int? FSCPercentID { get; set; }

        public bool IsDefault { get; set; }
    }
}
