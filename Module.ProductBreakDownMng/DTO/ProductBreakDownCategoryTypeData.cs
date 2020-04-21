using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class ProductBreakDownCategoryTypeData
    {
        public int ProductBreakDownCategoryTypeID { get; set; }

        public int? ProductBreakDownCategoryID { get; set; }

        public string ProductBreakDownCategoryTypeNM { get; set; }

        public List<DTO.ProductBreakDownDetailData> ProductBreakDownDetail { get; set; }

        public ProductBreakDownCategoryTypeData()
        {
            ProductBreakDownDetail = new List<ProductBreakDownDetailData>();
        }
    }
}
