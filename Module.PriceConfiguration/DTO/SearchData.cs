using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceConfiguration.DTO
{
    public class SearchData
    {
        public List<PriceConfigurationSearchResultDto> Data { get; set; }

        public List<Module.Support.DTO.Season> SupportSeason { get; set; }
        public List<Module.Support.DTO.ProductElement> SupportProductElement { get; set; }
    }
}
