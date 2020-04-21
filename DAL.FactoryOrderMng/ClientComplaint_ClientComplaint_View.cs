//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientComplaint_ClientComplaint_View
    {
        public ClientComplaint_ClientComplaint_View()
        {
            this.ClientComplaint_ClientComplaintItem_View = new HashSet<ClientComplaint_ClientComplaintItem_View>();
        }
    
        public int ClientComplaintID { get; set; }
        public string ComplaintNumber { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string YearSeason { get; set; }
        public Nullable<int> ComplaintType { get; set; }
        public Nullable<int> ComplaintStatus { get; set; }
        public Nullable<bool> ComplaintAccepted { get; set; }
        public Nullable<int> ApproverID { get; set; }
        public string ApproverFullName { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<bool> CreditNoteNeeded { get; set; }
        public Nullable<bool> SparepartNeeded { get; set; }
        public Nullable<bool> ReplacementNeeded { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
        public string ActionFollowUpBy { get; set; }
        public string CreatedPINumberSolution { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ContactPerson { get; set; }
        public string EANCodeCreated { get; set; }
        public string StatusOrderNetherlands { get; set; }
        public Nullable<System.DateTime> DeliveryDateClient { get; set; }
        public Nullable<int> UpdatorID { get; set; }
        public string UpdatorFullName { get; set; }
        public Nullable<int> CreatorID { get; set; }
        public string CreatorFullName { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string SaleNM { get; set; }
        public string Sale2NM { get; set; }
        public string SaleVnNM { get; set; }
    
        public virtual ICollection<ClientComplaint_ClientComplaintItem_View> ClientComplaint_ClientComplaintItem_View { get; set; }
    }
}
