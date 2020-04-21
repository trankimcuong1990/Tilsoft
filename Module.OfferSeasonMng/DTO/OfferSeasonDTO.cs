using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class OfferSeasonDTO
    {
        public int OfferSeasonID { get; set; }
        public string OfferSeasonUD { get; set; }
        public Nullable<int> OfferSeasonTypeID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Currency { get; set; }        
        public Nullable<decimal> VAT { get; set; }
        public string Remark { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string OfferSeasonTypeNM { get; set; }
        public string PaymentTermNM { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<int> InDays { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<decimal> DownValue { get; set; }
        public string DeliveryTermNM { get; set; }
        public string CreatorName { get; set; }
        public string SaleUD { get; set; }
        public string SaleNM { get; set; }
        public bool? IsClientEstimatedAdditionalCostConfirmed { get; set; }
        public Nullable<decimal> DefaultCommissionPercent { get; set; }
        public Nullable<decimal> EstTransportationPerContainerEUR { get; set; }
        public decimal? PiConfirmedDeltaLastYear { get; set; }

    }
}
