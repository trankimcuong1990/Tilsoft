using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.MISaleManagementRpt
{
    public class DDCProformaBySale
    {
        public string SaleUD { get; set; }
        public string SaleNM { get; set; }

        public decimal? ExpectedAmountInEUR { get; set; }
        public decimal? ExpectedUSD { get; set; }
        public decimal? ExpectedEUR { get; set; }

        public decimal? PIAmountInEUR { get; set; }
        public decimal? PiUSD { get; set; }
        public decimal? PiEUR { get; set; }

        public decimal? PIConfirmedAmountInEUR { get; set; }
        public decimal? PiConfirmedUSD { get; set; }
        public decimal? PiConfirmedEUR { get; set; }

        public decimal? CIAmountInEUR { get; set; }
        public decimal? CiUSD { get; set; }
        public decimal? CiEUR { get; set; }

        public decimal? CIFobAmountInEUR { get; set; }
        public decimal? CIWarehouseAmountInEUR { get; set; }
        public decimal? CIOtherAmountInEUR { get; set; }
        public decimal? CICreditNoteAmountInEUR { get; set; }
        public decimal? DeltaAfterAllInEUR { get; set; }
        public decimal? LCCostAmountInEUR { get; set; }
        public decimal? InterestAmountInEUR { get; set; }
        public decimal? PurchasingAmountInEUR { get; set; }
        public decimal? DeltaAfterAllPercent { get; set; }
    }
}