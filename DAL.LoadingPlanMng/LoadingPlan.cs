//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.LoadingPlanMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoadingPlan
    {
        public LoadingPlan()
        {
            this.LoadingPlanSparepartDetail = new HashSet<LoadingPlanSparepartDetail>();
            this.LoadingPlanDetail = new HashSet<LoadingPlanDetail>();
            this.DocumentClient = new HashSet<DocumentClient>();
            this.LoadingPlanSampleDetail = new HashSet<LoadingPlanSampleDetail>();
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
        public string ProductPicture1 { get; set; }
        public string ProductPicture2 { get; set; }
        public string ContainerPicture1 { get; set; }
        public string ContainerPicture2 { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string ContainerPicture3 { get; set; }
        public string ContainerPicture4 { get; set; }
        public string ContainerPicture5 { get; set; }
        public string ContainerPicture6 { get; set; }
        public string MerchandisesInside1Per3ContImage { get; set; }
        public string MerchandisesInside1Per2ContImage { get; set; }
        public string MerchandisesInsideFullContImage { get; set; }
        public string ContainerDoorAndLockImage { get; set; }
        public string ContainerSealImage { get; set; }
        public string DryPackImage { get; set; }
        public string ApprovedImage { get; set; }
        public Nullable<int> ControllerID { get; set; }
    
        public virtual ICollection<LoadingPlanSparepartDetail> LoadingPlanSparepartDetail { get; set; }
        public virtual ICollection<LoadingPlanDetail> LoadingPlanDetail { get; set; }
        public virtual ICollection<DocumentClient> DocumentClient { get; set; }
        public virtual ICollection<LoadingPlanSampleDetail> LoadingPlanSampleDetail { get; set; }
    }
}