using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.GrossMarginSummaryRpt
{
    public class HtmlGrossMarginSummary
    {
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SaleUD { get; set; }
        public decimal? NetAmountEUR { get; set; }
        public decimal? CostAmountEUR { get; set; }
        public decimal? CommAmountEUR { get; set; }
        public decimal? CreditNoteAmountEUR { get; set; }
        public decimal? GrossMarginBeforeEUR { get; set; }
        public decimal? GrossMarginBeforePercent { get; set; }
        public decimal? GrossMarginAfterCommEUR { get; set; }
        public decimal? GrossMarginAfterCommPercent { get; set; }
        public decimal? GrossMarginAfterCommAndCreditNoteEUR { get; set; }
        public decimal? GrossMarginAfterCommAndCreditNotePercent { get; set; }
    }
}