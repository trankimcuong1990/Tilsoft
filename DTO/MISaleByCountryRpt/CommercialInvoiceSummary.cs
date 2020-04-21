using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleByCountryRpt
{
    public class CommercialInvoiceSummary
    {
        public int? ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public string ClientCodeForLog { get; set; }

        public decimal? TotalCont_ThisYear { get; set; }
        public decimal? TotalEURAmount_ThisYear { get; set; }
        public int? TotalClient_ThisYear { get; set; }

        public decimal? ProformaInvoiceCont_ThisYear { get; set; }
        public decimal? ProformaInvoiceAmountEUR_ThisYear { get; set; }
        public int? ProformaInvoiceTotalClient_ThisYear { get; set; }

        public decimal? TotalCont_LastYear { get; set; }
        public decimal? TotalEURAmount_LastYear { get; set; }
        public int? TotalClient_LastYear { get; set; }

        public decimal? TotalCostEURAmount_LastYear { get; set; }
        public decimal? TotalMarginAmountEUR_LastYear { get; set; }

        public decimal? LCCostAmountEUR_LastYear { get; set; }
        public decimal? InterestAmountEUR_LastYear { get; set; }
        public decimal? PercentMargin_LastYear { get; set; }
        public decimal? PurchasingAmountInEUR { get; set; }

    }
}