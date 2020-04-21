using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakdownPriceListMng.DTO
{
    public class SearchFormData
    {
        public List<BreakdownPriceListSearchDTO> Data { get; set; }
        public int CompanyID { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
}
