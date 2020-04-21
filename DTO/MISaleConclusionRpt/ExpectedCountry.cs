using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleConclusionRpt
{
    public class ExpectedCountry
    {
        public int? ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public decimal? TotalExpectedAmount { get; set; }
        public decimal? TotalExpectedCont { get; set; }
        public int? TotalClient { get; set; }
        public decimal? TotalExpectedAmountLastYear { get; set; }
        public decimal? TotalCommercialInvoiceAmountLastYear { get; set; }
    }
}