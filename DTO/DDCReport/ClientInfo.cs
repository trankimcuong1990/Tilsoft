using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DDCReport
{
    public class ClientInfo
    {
        public int ClientID { get; set; }
        public int? SaleID { get; set; }
        public string SaleUD1 { get; set; }
        public string SaleUD2 { get; set; }
        public string SaleVNUD { get; set; }
        public string SaleVN2UD { get; set; }
        public string AgentUD3 { get; set; }
        public string PaymentTermNM { get; set; }
        public string ClientCountryNM { get; set; }
        public string HaveBizRelationshipFrom { get; set; }
        public string ClientTypeNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public decimal? MinUSD { get; set; }
        public decimal? AvgUSD { get; set; }
        public decimal? MaxUSD { get; set; }
        public decimal? MinEUR { get; set; }
        public decimal? AvgEUR { get; set; }
        public decimal? MaxEUR { get; set; }

        public decimal? WickerContQty { get; set; }
        public decimal? WickerPromoContQty { get; set; }
        public decimal? WoodAcaciaContQty { get; set; }
        public decimal? WoodTeakContQty { get; set; }
        public decimal? ChinaContQty { get; set; }
        public decimal? IndoContQty { get; set; }

        public decimal? PreTotalContWicker { get; set; }
        public decimal? PreTotalContWood { get; set; }
        public decimal? PreTotalContOther { get; set; }
        public decimal? PreTotalContCN { get; set; }
        public decimal? PreTotalContIN { get; set; }
        public decimal? PreTotalUSD { get; set; }
        public decimal? PreTotalEUR { get; set; }
        public decimal? PreTotalCont { get; set; }

        public decimal? TotalOrderCont { get; set; }
        public decimal? TotalOrderUSD { get; set; }
        public decimal? TotalOrderEUR { get; set; }
        public decimal? TotalConfirmedCont { get; set; }
        public decimal? TotalConfirmedUSD { get; set; }
        public decimal? TotalConfirmedEUR { get; set; }

        //calculate field
        public decimal? TotalOrderConverToEUR { get; set; }
        public decimal? TotalConfirmedConvertToEUR { get; set; }
        public decimal? ProformasTotalGAP { get; set; }
        public decimal? ProformasTotalRelativeGAP { get; set; }
        public decimal? TotalExpectedCont { get; set; }
        public decimal? TotalExpected { get; set; }
        public decimal? DeltaTurnOverLastYear { get; set; }
        public decimal? ExpectedPercent { get; set; }
        public decimal? DeltaTurnOver { get; set; }
        public decimal? ExpectedGAP { get; set; }
        public decimal? PreTotalConverToEUR { get; set; }
        public decimal? TotalContSpecification { get; set; }
        public decimal? TotalContWickerSuggest { get; set; }
        public decimal? TotalContWoodSuggest { get; set; }
        public decimal? TotalContOtherSuggest { get; set; }
        public decimal? TotalContCNSuggest { get; set; }
        public decimal? TotalContINSuggest { get; set; }
        public decimal? TotalContSuggest { get; set; }
        public decimal? GrossMarginPercent { get; set; }
        public decimal? GrossMarginAmount { get; set; }
        public decimal? PurchasingAmountUSD { get; set; }

        public decimal? OSGrossMarginPercent { get; set; }
        public decimal? OSGrossMarginAmount { get; set; }
        public decimal? OSPurchasingAmountUSD { get; set; }
        public decimal? TotalNetAmountUSD { get; set; }
        public decimal? TotalNetAmountEUR { get; set; }
    }
}
