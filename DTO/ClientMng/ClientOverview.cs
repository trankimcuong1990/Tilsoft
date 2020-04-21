using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientOverview
    {
        public int? ClientID { get; set; }
        public bool? IsActive { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientTypeNM { get; set; }
        public string ClientGroupNM { get; set; }
        public int? QntOfStore { get; set; }
        public string HaveBusinessRelationshipFromYear { get; set; }
        public int? TotalRelationShipYear { get; set; }
        public string ClientRelationshipTypeNM { get; set; }
        public string VATNo { get; set; }
        public string GLNNumber { get; set; }
        public string CreditSafeURL { get; set; }
        public string Language1NM { get; set; }
        public string Language2NM { get; set; }
        public string Language3NM { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public string ClientCityNM { get; set; }
        public string ClientCountryNM { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Website2 { get; set; }
        public string PaymentTermNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string AdditionalCondition { get; set; }
        public int? AccountManagerID { get; set; }
        public string AccountManagerName { get; set; }
        public int? SalesAssitantID { get; set; }
        public string SalesAssitantName { get; set; }
        public int? VNSaleAssistantID { get; set; }
        public string VNSaleAssistantName { get; set; }
        public int? VNSaleAssistant2ID { get; set; }
        public string VNSaleAssistant2Name { get; set; }
        public int? VNSaleMerchandiserAssistantID { get; set; }
        public string VNSaleMerchandiserAssistantName { get; set; }
        public string AgentNM { get; set; }
        public decimal? DefaultCommissionPercent { get; set; }
        public decimal? BonusPercent { get; set; }
        public string Remark { get; set; }
        public string Rating { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public string CreatedDate { get; set; }
        public bool? IsBuyingOrangePie { get; set; }
        public string BuyingOrganization { get; set; }
        public bool? CRF { get; set; }
        public int? ShippingLength { get; set; }
        public bool? IsExcludedInDeltaReport { get; set; }

        // Child list
        public List<AppointmentDTO> Appointments { get; set; }
        public List<ClientContact> ClientContacts { get; set; }
        public List<ClientContract> ClientContracts { get; set; }
        public List<ClientOverviewDDC> ClientDDCs { get; set; }
        public List<PenaltyTermDTO> PenaltyTerms { get; set; }
        //public List<ClientOffer> ClientOffers { get; set; }
        public DTO.ClientMng.Overview.CIS.ShippingPerformanceDataContainer ShippingPerformanceData { get; set; }
        public DTO.ClientMng.Overview.CIS.ItemDataContainer ItemData { get; set; }
        public DTO.ClientMng.Overview.CIS.PriceComparisonDataContainer PriceComparisonData { get; set; }

        // Season supported.
        public List<Support.Season> Seasons { get; set; }

        //chart data
        public List<ChartCommercialInvoice> ChartCommercialInvoices { get; set; }
        public bool HasPermissionOnCommercialInvoiceChart { get; set; }
        public List<DTO.ClientMng.ClientEstimatedAdditionalCostDTO> ClientEstimatedAdditionalCost { get; set; }

        public List<ClientComplaintStatusDTO> ClientComplaintStatuses { get; set; }
        public List<ClientComplaintTypeDTO> ClientComplaintTypes { get; set; }
        public List<ClientAdditionalConditionDTO> ClientAdditionalConditionDTO { get; set; }
        public List<ClientContactHistory> ClientContactHistories { get; set; }
        public List<ClientBusinessCardDTO> ClientBusinessCardDTO { get; set; }
        public List<ClientAdditionalConditionOverviewDTO> ClientAdditionalConditionOverviewDTO { get; set; }
    }
}
