using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceQuotationMng.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.Season> Season { get; set; }
        public List<Support.DTO.PriceDifference> PriceDifference { get; set; }
        public List<Support.DTO.QuotationStatus> QuotationStatus { get; set; }
    }
}
