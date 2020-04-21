using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DeltaByClientCompare
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<int> Sale2ID { get; set; }
        public string SaleUD { get; set; }
        public string SaleNM { get; set; }

        public Nullable<decimal> PiConfirmedDelta5AmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta7AmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountLastYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountLastYear { get; set; }        
        public Nullable<decimal> PiConfirmedDelta5PercentLastYear { get; set; }
        public Nullable<decimal> PiConfirmedContLastYear { get; set; }

        public Nullable<decimal> OfferDelta5AmountThisYear { get; set; }
        public Nullable<decimal> OfferDelta7AmountThisYear { get; set; }
        public Nullable<decimal> OfferPurchasingAmountThisYear { get; set; }
        public Nullable<decimal> OfferSaleAmountThisYear { get; set; }
        public Nullable<decimal> OfferDelta5PercentThisYear { get; set; }
        public Nullable<decimal> OfferContThisYear { get; set; }

        public Nullable<decimal> PiDelta5AmountThisYear { get; set; }
        public Nullable<decimal> PiDelta7AmountThisYear { get; set; }
        public Nullable<decimal> PiPurchasingAmountThisYear { get; set; }
        public Nullable<decimal> PiSaleAmountThisYear { get; set; }
        public Nullable<decimal> PiDelta5PercentThisYear { get; set; }
        public Nullable<decimal> PiContThisYear { get; set; }

        public Nullable<decimal> PiConfirmedDelta5AmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta7AmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedPurchasingAmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedSaleAmountThisYear { get; set; }
        public Nullable<decimal> PiConfirmedDelta5PercentThisYear { get; set; }
        public Nullable<decimal> PiConfirmedContThisYear { get; set; }

        public decimal? DeltaOfDelta
        {
            get {
                if (!OfferDelta5PercentThisYear.HasValue || OfferDelta5PercentThisYear.Value == 0 || !PiConfirmedDelta5PercentLastYear.HasValue || PiConfirmedDelta5PercentLastYear.Value <= 0)
                {
                    return 0;
                }
                else
                {
                    return OfferDelta5PercentThisYear - PiConfirmedDelta5PercentLastYear;
                }                
            }
        }

        public decimal? DeltaOfDelta2
        {
            get
            {
                if (!PiDelta5PercentThisYear.HasValue || PiDelta5PercentThisYear.Value == 0 || !PiConfirmedDelta5PercentLastYear.HasValue || PiConfirmedDelta5PercentLastYear.Value <= 0)
                {
                    return 0;
                }
                else
                {
                    return PiDelta5PercentThisYear - PiConfirmedDelta5PercentLastYear;
                }
            }
        }

        public decimal? DeltaOfDelta3
        {
            get
            {
                if (!PiConfirmedDelta5PercentThisYear.HasValue || PiConfirmedDelta5PercentThisYear.Value == 0 || !PiConfirmedDelta5PercentLastYear.HasValue || PiConfirmedDelta5PercentLastYear.Value <= 0)
                {
                    return 0;
                }
                else
                {
                    return PiConfirmedDelta5PercentThisYear - PiConfirmedDelta5PercentLastYear;
                }
            }
        }
    }
}
