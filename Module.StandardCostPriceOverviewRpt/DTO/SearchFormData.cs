using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StandardCostPriceOverviewRpt.DTO
{
   public class SearchFormData
    {
        public List<DTO.ProductSearchResultDto> Data { get; set; }
        public List<Support.DTO.Season> Sdata { get; set; } //call Season
        public List<DTO.ProductPrice> DataPrice { get; set; }
    }
}
