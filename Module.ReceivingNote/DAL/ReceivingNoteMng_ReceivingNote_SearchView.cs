//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ReceivingNote.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivingNoteMng_ReceivingNote_SearchView
    {
        public int ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public Nullable<System.DateTime> ReceivingNoteDate { get; set; }
        public string PurchasingOrderNo { get; set; }
        public string WorkCenterNM { get; set; }
        public string FromProductionTeamNM { get; set; }
        public string Description { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ViewName { get; set; }
        public Nullable<int> ReceivingNoteTypeID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string DeliverName { get; set; }
        public string DeliverAddress { get; set; }
        public Nullable<int> WorkCenterID { get; set; }
        public Nullable<int> FromProductionTeamID { get; set; }
        public Nullable<bool> IsConfirm { get; set; }
        public Nullable<int> ConfirmBy { get; set; }
        public Nullable<System.DateTime> ConfirmDate { get; set; }
        public string ConfirmedName { get; set; }
        public string WorkOrderIDs { get; set; }
        public Nullable<int> ResetBy { get; set; }
        public Nullable<System.DateTime> ResetDate { get; set; }
        public string ReseterName { get; set; }
        public Nullable<int> StatusTypeID { get; set; }
    }
}