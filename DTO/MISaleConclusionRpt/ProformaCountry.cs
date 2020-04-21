using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleConclusionRpt
{
    public class ProformaCountry
    {
        public int ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public decimal TotalProformaAmount { get; set; }
        public decimal TotalProformaCont { get; set; }
        public int TotalClient { get; set; }
        public decimal TotalProformaAmountLastYear { get; set; }
        public decimal TotalCommercialInvoiceAmountLastYear { get; set; }
        public decimal DeltaAmount { get; set; }
        public Nullable<decimal> DeltaPercent { get; set; }
        public decimal PurchasingAmountUSD { get; set; }
    }
}