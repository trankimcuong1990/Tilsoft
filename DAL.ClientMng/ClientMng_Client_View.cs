//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientMng_Client_View
    {
        public ClientMng_Client_View()
        {
            this.ClientMng_ClientContact_View = new HashSet<ClientMng_ClientContact_View>();
            this.ClientMng_ClientContactHistory_View = new HashSet<ClientMng_ClientContactHistory_View>();
            this.ClientMng_DDCDetail_View = new HashSet<ClientMng_DDCDetail_View>();
            this.ClientMng_ClientContract_View = new HashSet<ClientMng_ClientContract_View>();
            this.ClientMng_SaleOrder_View = new HashSet<ClientMng_SaleOrder_View>();
            this.ClientMng_Appointment_View = new HashSet<ClientMng_Appointment_View>();
            this.ClientMng_ClientSpecialPackagingMethod_View = new HashSet<ClientMng_ClientSpecialPackagingMethod_View>();
            this.ClientMng_ClientEstimatedAdditionalCost_View = new HashSet<ClientMng_ClientEstimatedAdditionalCost_View>();
            this.ClientMng_PenaltyTerm_View = new HashSet<ClientMng_PenaltyTerm_View>();
            this.ClientMng_ClientBusinessCard_View = new HashSet<ClientMng_ClientBusinessCard_View>();
            this.ClientMng_ClientAdditionalCondition_View = new HashSet<ClientMng_ClientAdditionalCondition_View>();
        }
    
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> ClientGroupID { get; set; }
        public Nullable<int> RelationshipTypeID { get; set; }
        public Nullable<int> ClientTypeID { get; set; }
        public string VATNo { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> ClientCityID { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string HaveBizRelationshipFrom { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<int> Sale2ID { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<System.DateTime> EstablishmentDate { get; set; }
        public string Rating { get; set; }
        public string GLNNumber { get; set; }
        public string Website { get; set; }
        public Nullable<int> QntOfStore { get; set; }
        public string HaveBusinessRelationshipFromYear { get; set; }
        public string LogoImage { get; set; }
        public Nullable<int> Language1ID { get; set; }
        public Nullable<int> Language2ID { get; set; }
        public Nullable<int> Language3ID { get; set; }
        public Nullable<int> AgentID { get; set; }
        public string CreditSafeURL { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string SaleNM { get; set; }
        public string AgentNM { get; set; }
        public string AdditionalCondition { get; set; }
        public Nullable<int> SaleVNID { get; set; }
        public Nullable<int> CITemplateID { get; set; }
        public Nullable<int> PITemplateID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public Nullable<bool> IsBuyingOrangePie { get; set; }
        public string BuyingOrganization { get; set; }
        public Nullable<int> SaleVN2ID { get; set; }
        public string Saler2NM { get; set; }
        public string SalerVNNM { get; set; }
        public string Saler2VNNM { get; set; }
        public string SubmissionNL { get; set; }
        public Nullable<int> SaleMerAssistID { get; set; }
        public string SalerMerAssistNM { get; set; }
        public Nullable<bool> CRF { get; set; }
        public Nullable<int> ShippingLength { get; set; }
        public Nullable<bool> IsExcludedInDeltaReport { get; set; }
        public string Website2 { get; set; }
    
        public virtual ICollection<ClientMng_ClientContact_View> ClientMng_ClientContact_View { get; set; }
        public virtual ICollection<ClientMng_ClientContactHistory_View> ClientMng_ClientContactHistory_View { get; set; }
        public virtual ICollection<ClientMng_DDCDetail_View> ClientMng_DDCDetail_View { get; set; }
        public virtual ICollection<ClientMng_ClientContract_View> ClientMng_ClientContract_View { get; set; }
        public virtual ICollection<ClientMng_SaleOrder_View> ClientMng_SaleOrder_View { get; set; }
        public virtual ICollection<ClientMng_Appointment_View> ClientMng_Appointment_View { get; set; }
        public virtual ICollection<ClientMng_ClientSpecialPackagingMethod_View> ClientMng_ClientSpecialPackagingMethod_View { get; set; }
        public virtual ICollection<ClientMng_ClientEstimatedAdditionalCost_View> ClientMng_ClientEstimatedAdditionalCost_View { get; set; }
        public virtual ICollection<ClientMng_PenaltyTerm_View> ClientMng_PenaltyTerm_View { get; set; }
        public virtual ICollection<ClientMng_ClientBusinessCard_View> ClientMng_ClientBusinessCard_View { get; set; }
        public virtual ICollection<ClientMng_ClientAdditionalCondition_View> ClientMng_ClientAdditionalCondition_View { get; set; }
    }
}