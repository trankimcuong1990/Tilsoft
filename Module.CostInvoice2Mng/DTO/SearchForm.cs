using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DTO
{
    public class SearchForm
    {
        public decimal? TotalAmountValue { get; set; }
        public decimal? SubTotalAmountValue { get; set; }
        public List<CostInvoice2SearchResult> CostInvoice2SearchResults { get; set; }
    }
}
