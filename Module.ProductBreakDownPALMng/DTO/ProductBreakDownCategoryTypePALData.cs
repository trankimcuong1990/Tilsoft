using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class ProductBreakDownCategoryTypePALData
    {
        public int ProductBreakDownCategoryTypeID { get; set; }
        public int? ProductBreakDownCategoryID { get; set; }
        public string ProductBreakDownCategoryTypeNM { get; set; }

        public List<ProductBreakDownDetailPALData> ProductBreakDownDetailPAL { get; set; }

        public ProductBreakDownCategoryTypePALData()
        {
            ProductBreakDownDetailPAL = new List<ProductBreakDownDetailPALData>();
        }
    }
}
