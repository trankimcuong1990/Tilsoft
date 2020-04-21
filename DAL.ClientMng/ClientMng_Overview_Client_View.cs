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
    
    public partial class ClientMng_Overview_Client_View
    {
        public ClientMng_Overview_Client_View()
        {
            this.ClientMng_Overview_Appointment_View = new HashSet<ClientMng_Overview_Appointment_View>();
            this.ClientMng_Overview_ClientContract_View = new HashSet<ClientMng_Overview_ClientContract_View>();
            this.ClientMng_Overview_DDC_View = new HashSet<ClientMng_Overview_DDC_View>();
            this.ClientMng_Overview_ECommercialInvoice_View = new HashSet<ClientMng_Overview_ECommercialInvoice_View>();
            this.ClientMng_Overview_Offer_View = new HashSet<ClientMng_Overview_Offer_View>();
            this.ClientMng_Overview_SaleOrder_View = new HashSet<ClientMng_Overview_SaleOrder_View>();
            this.ClientMng_Overview_SampleOrder_View = new HashSet<ClientMng_Overview_SampleOrder_View>();
            this.ClientMng_Overview_PLC_View = new HashSet<ClientMng_Overview_PLC_View>();
            this.ClientMng_Overview_ClientContact_View = new HashSet<ClientMng_Overview_ClientContact_View>();
            this.ClientMng_Overview_ClientComplaint_View = new HashSet<ClientMng_Overview_ClientComplaint_View>();
            this.ClientMng_OverView_PenaltyTerm_View = new HashSet<ClientMng_OverView_PenaltyTerm_View>();
            this.ClientMng_Overview_ClientContactHistory_View = new HashSet<ClientMng_Overview_ClientContactHistory_View>();
            this.ClientMng_Overview_ClientBusinessCard_View = new HashSet<ClientMng_Overview_ClientBusinessCard_View>();
            this.ClientMng_Overview_ClientAdditionalCondition_View = new HashSet<ClientMng_Overview_ClientAdditionalCondition_View>();
        }
    
        public int ClientID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientTypeNM { get; set; }
        public string ClientGroupNM { get; set; }
        public Nullable<int> QntOfStore { get; set; }
        public string HaveBusinessRelationshipFromYear { get; set; }
        public Nullable<int> TotalRelationShipYear { get; set; }
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
        public string PaymentTermNM { get; set; }
        public string DeliveryTermNM { get; set; }
        public string AdditionalCondition { get; set; }
        public Nullable<int> AccountManagerID { get; set; }
        public string AccountManagerName { get; set; }
        public Nullable<int> SalesAssitantID { get; set; }
        public string SalesAssitantName { get; set; }
        public Nullable<int> VNSaleAssistantID { get; set; }
        public string VNSaleAssistantName { get; set; }
        public string AgentNM { get; set; }
        public string Remark { get; set; }
        public string Rating { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsBuyingOrangePie { get; set; }
        public string BuyingOrganization { get; set; }
        public Nullable<int> VNSaleAssistant2ID { get; set; }
        public string VNSaleAssistant2Name { get; set; }
        public Nullable<int> VNSaleMerchandiserAssistantID { get; set; }
        public string VNSaleMerchandiserAssistantName { get; set; }
        public Nullable<bool> CRF { get; set; }
        public Nullable<int> ShippingLength { get; set; }
        public Nullable<decimal> DefaultCommissionPercent { get; set; }
        public Nullable<bool> IsExcludedInDeltaReport { get; set; }
        public Nullable<decimal> BonusPercent { get; set; }
        public string Website2 { get; set; }
    
        public virtual ICollection<ClientMng_Overview_Appointment_View> ClientMng_Overview_Appointment_View { get; set; }
        public virtual ICollection<ClientMng_Overview_ClientContract_View> ClientMng_Overview_ClientContract_View { get; set; }
        public virtual ICollection<ClientMng_Overview_DDC_View> ClientMng_Overview_DDC_View { get; set; }
        public virtual ICollection<ClientMng_Overview_ECommercialInvoice_View> ClientMng_Overview_ECommercialInvoice_View { get; set; }
        public virtual ICollection<ClientMng_Overview_Offer_View> ClientMng_Overview_Offer_View { get; set; }
        public virtual ICollection<ClientMng_Overview_SaleOrder_View> ClientMng_Overview_SaleOrder_View { get; set; }
        public virtual ICollection<ClientMng_Overview_SampleOrder_View> ClientMng_Overview_SampleOrder_View { get; set; }
        public virtual ICollection<ClientMng_Overview_PLC_View> ClientMng_Overview_PLC_View { get; set; }
        public virtual ICollection<ClientMng_Overview_ClientContact_View> ClientMng_Overview_ClientContact_View { get; set; }
        public virtual ICollection<ClientMng_Overview_ClientComplaint_View> ClientMng_Overview_ClientComplaint_View { get; set; }
        public virtual ICollection<ClientMng_OverView_PenaltyTerm_View> ClientMng_OverView_PenaltyTerm_View { get; set; }
        public virtual ICollection<ClientMng_Overview_ClientContactHistory_View> ClientMng_Overview_ClientContactHistory_View { get; set; }
        public virtual ICollection<ClientMng_Overview_ClientBusinessCard_View> ClientMng_Overview_ClientBusinessCard_View { get; set; }
        public virtual ICollection<ClientMng_Overview_ClientAdditionalCondition_View> ClientMng_Overview_ClientAdditionalCondition_View { get; set; }
    }
}
