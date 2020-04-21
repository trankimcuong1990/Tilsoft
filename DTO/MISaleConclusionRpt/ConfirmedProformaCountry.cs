using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleConclusionRpt
{
    public class ConfirmedProformaCountry
    {
        public int? ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public decimal? TotalConfirmedProformaAmount { get; set; }
        public decimal? TotalConfirmedProformaCont { get; set; }
        public int? TotalClient { get; set; }
        public decimal? TotalConfirmedProformaAmountLastYear { get; set; }
        public decimal? TotalCommercialInvoiceAmountLastYear { get; set; }
        public decimal? DeltaAmount { get; set; }
        public decimal? DeltaPercent { get; set; }
        public Nullable<decimal> PurchasingAmountUSD { get; set; }

    }
}