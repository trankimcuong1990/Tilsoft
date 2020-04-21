using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class SearchFormData
    {
        public List<ProductBreakDownDefaultCategoryPALSearchResultData> ProductBreakDownDefaultCategoryPALSearchResult { get; set; }
        public List<ProductBreakDownPALSearchResultData> ProductBreakDownPALSearchResult { get; set; }

        public SearchFormData(int getTypeID)
        {
            if (getTypeID == 1)
            {
                ProductBreakDownDefaultCategoryPALSearchResult = new List<ProductBreakDownDefaultCategoryPALSearchResultData>();
            }

            if (getTypeID == 2)
            {
                ProductBreakDownPALSearchResult = new List<ProductBreakDownPALSearchResultData>();
            }
        }
    }
}
