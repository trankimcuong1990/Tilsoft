using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ProductBreakDown
    {
        public int ProductBreakDownID { get; set; }
        public int? ModelID { get; set; }
        public int? SampleProductID { get; set; }
        public string ItemSize { get; set; }
        public string CartonSize { get; set; }
        public string FrameDescription { get; set; }
        public string CushionDescription { get; set; }
        public int? UpdatedBy { get; set; }
        public string Remark { get; set; }
        public string UpdatedDate { get; set; }
        public int? ProductBreakDownCategoryID { get; set; }
        public int? FactoryID { get; set; }
    }
}
