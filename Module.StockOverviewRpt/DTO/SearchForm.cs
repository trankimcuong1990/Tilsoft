using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.StockOverviewRpt.DTO
{
    public class SearchForm
    {
        public List<DTO.StockDTO> StockDTOs { get; set; }
        public List<DTO.ProductType> ProductTypes { get; set; }
    }
}
