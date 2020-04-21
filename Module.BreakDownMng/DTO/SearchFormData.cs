using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class SearchFormData
    {
        public List<BreakDownSearchDTO> Data { get; set; }
        public decimal? Exchange { get; set; }
        public List<CaculatePriceUSD> CaculatePriceUSDs { get; set; }
        public List<CalculationPriceMangementCost> CalculationPriceMangementCosts { get; set; }        

        public int TotalAVFConfirmedItem { get; set; }
        public int TotalAVTConfirmedItem { get; set; }
    }
}
