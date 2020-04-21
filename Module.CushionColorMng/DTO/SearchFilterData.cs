using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionColorMng.DTO
{
    public class SearchFilterData
    {
        public List<DTO.Season> Seasons { get; set; }
        public List<DTO.YesNo> YesNoValues { get; set; }
        public List<DTO.ProductGroup> ProductGroups { get; set; }
        public List<DTO.CushionType> CushionTypes { get; set; }
    }

    public class ProductGroup
    {
        public int ProductGroupID { get; set; }
        public string ProductGroupNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}
