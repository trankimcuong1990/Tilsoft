using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuotationMng
{
    public class SearchFormData
    {
        public List<QuotationSearchResult> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
