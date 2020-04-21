using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class SearchFormData
    {
        public List<DTO.ProductBreakDownDefaultCategorySearchResultData> DefaultData { get; set; }

        public List<DTO.ProductBreakDownSearchResultData> MainData { get; set; }
    }
}
