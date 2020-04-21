using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuotationMng
{
    public class SearchFilterData
    {
        public List<Support.Season> Seasons { get; set; }
        public List<Support.Factory> Factories { get; set; }
    }
}
