//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryLoadingPlanMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoadingPlan
    {
        public LoadingPlan()
        {
            this.LoadingPlanDetail = new HashSet<LoadingPlanDetail>();
            this.LoadingPlanSparepartDetail = new HashSet<LoadingPlanSparepartDetail>();
        }
    
        public int LoadingPlanID { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ContainerRefNo { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string ControllerName { get; set; }
        public Nullable<int> BookingID { get; set; }
        public string ContainerNo { get; set; }
        public Nullable<int> ContainerTypeID { get; set; }
        public string SealNo { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsLoaded { get; set; }
        public Nullable<System.DateTime> LoadingDate { get; set; }
        public Nullable<bool> IsShipped { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string IsSent { get; set; }
        public Nullable<int> SentBy { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public string Remark { get; set; }
        public string ProductPicture1 { get; set; }
        public string ProductPicture2 { get; set; }
        public string ContainerPicture1 { get; set; }
        public string ContainerPicture2 { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> ParentID { get; set; }
    
        public virtual ICollection<LoadingPlanDetail> LoadingPlanDetail { get; set; }
        public virtual ICollection<LoadingPlanSparepartDetail> LoadingPlanSparepartDetail { get; set; }
    }
}
