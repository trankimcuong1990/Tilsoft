using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class SearchFormData
    {
        public List<DTO.ClientMng.ClientSearchResult> Data { get; set; }        
        public bool HasPermissionOnGrossMargin { get; set; }
        public decimal? ExRate { get; set; }

        //delta
        public decimal? PiConfirmedContThisYear_Total { get; set; }
        public decimal? PiConfirmedSaleAmountThisYear_Total { get; set; }
        public decimal? PiConfirmedDelta9AmountThisYear_Total { get; set; }
        public decimal? PiConfirmedDelta9PercentThisYear_Total { get; set; }
        public decimal? PiConfirmedPurchasingAmountThisYear_Total { get; set; }

        public decimal? PiConfirmedContThisYear_SubTotal { get; set; }
        public decimal? PiConfirmedSaleAmountThisYear_SubTotal { get; set; }
        public decimal? PiConfirmedDelta9AmountThisYear_SubTotal { get; set; }
        public decimal? PiConfirmedDelta9PercentThisYear_SubTotal { get; set; }
        public decimal? PiConfirmedPurchasingAmountThisYear_SubTotal { get; set; }



    }
}
