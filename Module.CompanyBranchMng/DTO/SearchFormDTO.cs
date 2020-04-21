using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng.DTO
{
    public class SearchFormDTO
    {
        public List<CompanySearchResultDTO> Data { get; set; }

        public SearchFormDTO()
        {
            Data = new List<CompanySearchResultDTO>();
        }
    }
}
