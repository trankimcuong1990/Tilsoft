using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockStatusQntRpt.DTO
{
    public class SearchFormDTO
    {
        public List<StockStatusQntDTO> stockStatusQntDTOs { get; set; }

        public SearchFormDTO()
        {
            stockStatusQntDTOs = new List<StockStatusQntDTO>();
        }

    }
}
