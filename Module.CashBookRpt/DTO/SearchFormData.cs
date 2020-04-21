using System.Collections.Generic;

namespace Module.CashBookRpt.DTO
{
    public class SearchFormData
    {
        public List<CashBookRpt_SearchResultDto> CashBookRpt_SearchResultDtos { get; set; }
        public decimal? Beginning { get; set; }
    }
}
