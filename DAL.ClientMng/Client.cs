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
    
    public partial class Client
    {
        public Client()
        {
            this.DDCDetail = new HashSet<DDCDetail>();
            this.ClientContact = new HashSet<ClientContact>();
            this.ClientContract = new HashSet<ClientContract>();
            this.ClientSpecialPackagingMethod = new HashSet<ClientSpecialPackagingMethod>();
            this.ClientEstimatedAdditionalCost = new HashSet<ClientEstimatedAdditionalCost>();
            this.PenaltyTerm = new HashSet<PenaltyTerm>();
            this.Offer = new HashSet<Offer>();
            this.ClientAdditionalCondition = new HashSet<ClientAdditionalCondition>();
            this.ClientContactHistory = new HashSet<ClientContactHistory>();
            this.ClientBusinessCard = new HashSet<ClientBusinessCard>();
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
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> Sale2ID { get; set; }
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
        public Nullable<bool> IsActive { get; set; }
        public string AdditionalCondition { get; set; }
        public Nullable<int> SaleVNID { get; set; }
        public Nullable<int> CITemplateID { get; set; }
        public Nullable<int> PITemplateID { get; set; }
        public Nullable<bool> IsBuyingOrangePie { get; set; }
        public string BuyingOrganization { get; set; }
        public Nullable<int> SaleVN2ID { get; set; }
        public string SubmissionNL { get; set; }
        public Nullable<int> SaleMerAssistID { get; set; }
        public Nullable<bool> CRF { get; set; }
        public Nullable<int> ShippingLength { get; set; }
        public Nullable<bool> IsExcludedInDeltaReport { get; set; }
        public string Website2 { get; set; }
    
        public virtual ICollection<DDCDetail> DDCDetail { get; set; }
        public virtual ICollection<ClientContact> ClientContact { get; set; }
        public virtual ICollection<ClientContract> ClientContract { get; set; }
        public virtual ICollection<ClientSpecialPackagingMethod> ClientSpecialPackagingMethod { get; set; }
        public virtual ICollection<ClientEstimatedAdditionalCost> ClientEstimatedAdditionalCost { get; set; }
        public virtual ICollection<PenaltyTerm> PenaltyTerm { get; set; }
        public virtual ICollection<Offer> Offer { get; set; }
        public virtual ICollection<ClientAdditionalCondition> ClientAdditionalCondition { get; set; }
        public virtual ICollection<ClientContactHistory> ClientContactHistory { get; set; }
        public virtual ICollection<ClientBusinessCard> ClientBusinessCard { get; set; }
    }
}
