using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientMng
{
    public class ClientSearchResult
    {
        public int ClientID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public decimal? TotalConfirmedPIEURAmount { get; set; }
        public decimal? TotalQnt40HC { get; set; }
        public int? SaleUserID { get; set; }
        public string SaleNM { get; set; }
        public int? Sale2UserID { get; set; }
        public string Sale2NM { get; set; }
        public int? SaleVNUserID { get; set; }
        public string SaleVNName { get; set; }

        public int? SaleVN2UserID { get; set; }
        public string SaleVN2Name { get; set; }

        public string AgentNM { get; set; }
        public string HaveBusinessRelationshipFromYear { get; set; }
        public string Rating { get; set; }
        public int? QntOfStore { get; set; }
        public string VATNo { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string DeliveryTermNM { get; set; }
        public string PaymentTermNM { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? AgentID { get; set; }
        public int? PaymentTermID { get; set; }
        public int? DeliveryTermID { get; set; }
        public bool? IsActive { get; set; }
        public int? SaleID { get; set; }
        public int? Sale2ID { get; set; }
        public int? SaleVNID { get; set; }
        public bool? IsBuyingOrangePie { get; set; }
        public string BuyingOrganization { get; set; }
        public string ClientRelationshipTypeNM { get; set; }
        public int? RelationshipTypeID { get; set; }
        public decimal? GrossMarginAfterCommission { get; set; }
        public decimal? GrossMarginAfterCommissionPercent { get; set; }
        public decimal? PriceOverviewOrderAmount { get; set; }
        public int? SaleVN2ID { get; set; }
        public int? SaleMerAssistID { get; set; }
        public string SaleMerAssistNM { get; set; }

        //delta field
        public decimal? PiConfirmedContThisYear { get; set; }
        public decimal? PiConfirmedSaleAmountThisYear { get; set; }
        public decimal? PiConfirmedDelta9AmountThisYear { get; set; }
        public decimal? PiConfirmedDelta9PercentThisYear { get; set; }
        public decimal? PiConfirmedPurchasingAmountThisYear { get; set; }
    }
}