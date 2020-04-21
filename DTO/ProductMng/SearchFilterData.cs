using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class SearchFilterData
    {
        public List<Support.YesNo> ConfirmStatuses { get; set; }
        public List<Support.Season> Seasons { get; set; }
        public List<Support.ProductType> ProductTypes { get; set; }
    }
}
