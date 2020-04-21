using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ClientMng
{
    public class Client
    {
        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "ClientGroupID")]
        public int? ClientGroupID { get; set; }

        [Display(Name = "RelationshipTypeID")]
        public int? RelationshipTypeID { get; set; }

        [Display(Name = "ClientTypeID")]
        public int? ClientTypeID { get; set; }

        [Display(Name = "VATNo")]
        public string VATNo { get; set; }

        [Display(Name = "StreetAddress1")]
        public string StreetAddress1 { get; set; }

        [Display(Name = "StreetAddress2")]
        public string StreetAddress2 { get; set; }

        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        [Display(Name = "ClientCityID")]
        public int? ClientCityID { get; set; }

        [Display(Name = "ClientCountryID")]
        public int? ClientCountryID { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "HaveBizRelationshipFrom")]
        public string HaveBizRelationshipFrom { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "SaleID")]
        public int? SaleID { get; set; }

        [Display(Name = "PaymentTermID")]
        public int? PaymentTermID { get; set; }

        [Display(Name = "DeliveryTermID")]
        public int? DeliveryTermID { get; set; }

        [Display(Name = "Sale2ID")]
        public int? Sale2ID { get; set; }

        [Display(Name = "EstablishmentDate")]
        public DateTime? EstablishmentDate { get; set; }

        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [Display(Name = "GLNNumber")]
        public string GLNNumber { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Website2")]
        public string Website2 { get; set; }

        [Display(Name = "QntOfStore")]
        public int? QntOfStore { get; set; }

        [Display(Name = "HaveBusinessRelationshipFromYear")]
        public string HaveBusinessRelationshipFromYear { get; set; }

        [Display(Name = "LogoImage")]
        public string LogoImage { get; set; }
        public bool HasChanged { get; set; }
        public string NewFile { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }

        [Display(Name = "Language1ID")]
        public int? Language1ID { get; set; }

        [Display(Name = "Language2ID")]
        public int? Language2ID { get; set; }

        [Display(Name = "Language3ID")]
        public int? Language3ID { get; set; }

        [Display(Name = "AgentID")]
        public int? AgentID { get; set; }

        [Display(Name = "CreditSafeURL")]
        public string CreditSafeURL { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "SaleNM")]
        public string SaleNM { get; set; }

        [Display(Name = "AgentNM")]
        public string AgentNM { get; set; }

        [Display(Name = "SaleVNID")]
        public int SaleVNID { get; set; }

        [Display(Name = "SubmissionNL")]
        public string SubmissionNL { get; set; }

        public string AdditionalCondition { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public string CreatedDateFormated { get; set; }
        public string UpdatedDateFormated { get; set; }

        public List<ClientContact> ClientContacts { get; set; }
        public List<ClientContactHistory> ClientContactHistories { get; set; }
        public List<DDCDetail> DDCDetails { get; set; }
        public List<ClientContract> ClientContracts { get; set; }
        public List<SaleOrder> SaleOrders { get; set; }
        public List<AppointmentDTO> AppointmentDTOs { get; set; }
        public List<ClientSpecialPackagingMethod> ClientSpecialPackagingMethods { get; set; }
        public List<PenaltyTermDTO> PenaltyTerms { get; set; }
        public List<ClientGICComment> ClientGICComment { get; set; }

        public bool IsActive { get; set; }

        public int CITemplateID { get; set; }
        public int PITemplateID { get; set; }
        public bool? IsBuyingOrangePie { get; set; }
        public string BuyingOrganization { get; set; }
        public bool? CRF { get; set; }
        public int? ShippingLength { get; set; }

        [Display(Name = "SaleVN2ID")]
        public int? SaleVN2ID { get; set; }

        public string Saler2NM { get; set; }
        public string SalerVNNM { get; set; }
        public string Saler2VNNM { get; set; }

        public int? SaleMerAssistID { get; set; }
        public string SalerMerAssistNM { get; set; }

        public bool HasSpecialPermission { get; set; }

        public bool? IsExcludedInDeltaReport { get; set; }
        public bool? AutoGenerateCode { get; set; }

        public List<DTO.ClientMng.ClientEstimatedAdditionalCostDTO> ClientEstimatedAdditionalCost { get; set; }
        public List<DTO.ClientMng.ClientAdditionalConditionDTO> ClientAdditionalConditionDTO { get; set; }
        public List<DTO.ClientMng.ClientBusinessCardDTO> ClientBusinessCardDTO { get; set; }
    }
}