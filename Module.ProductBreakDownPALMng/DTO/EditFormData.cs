using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class EditFormData
    {
        public ProductBreakDownDefaultCategoryPALData ProductBreakDownDefaultCategoryPAL { get; set; }
        public ProductBreakDownPALData ProductBreakDownPAL { get; set; }

        public EditFormData(int getTypeID)
        {
            if (getTypeID == 1)
            {
                ProductBreakDownDefaultCategoryPAL = new ProductBreakDownDefaultCategoryPALData();
            }

            if (getTypeID == 2)
            {
                ProductBreakDownPAL = new ProductBreakDownPALData();
            }
        }
    }
}
