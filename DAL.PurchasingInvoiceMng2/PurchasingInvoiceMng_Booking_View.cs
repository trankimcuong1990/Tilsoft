//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PurchasingInvoiceMng2
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasingInvoiceMng_Booking_View
    {
        public PurchasingInvoiceMng_Booking_View()
        {
            this.PurchasingInvoiceMng_LoadingPlanDetail_View = new HashSet<PurchasingInvoiceMng_LoadingPlanDetail_View>();
            this.PurchasingInvoiceMng_LoadingPlanSparepartDetail_View = new HashSet<PurchasingInvoiceMng_LoadingPlanSparepartDetail_View>();
            this.PurchasingInvoiceMng_LoadingPlanSampleDetail_View = new HashSet<PurchasingInvoiceMng_LoadingPlanSampleDetail_View>();
        }
    
        public int BookingID { get; set; }
        public string BLNo { get; set; }
        public Nullable<System.DateTime> ShipedDate { get; set; }
        public string SupplierNM { get; set; }
        public string ForwarderNM { get; set; }
        public string Feeder { get; set; }
        public string POLName { get; set; }
        public string PODName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<bool> IsBuyingOrangePie { get; set; }
    
        public virtual ICollection<PurchasingInvoiceMng_LoadingPlanDetail_View> PurchasingInvoiceMng_LoadingPlanDetail_View { get; set; }
        public virtual ICollection<PurchasingInvoiceMng_LoadingPlanSparepartDetail_View> PurchasingInvoiceMng_LoadingPlanSparepartDetail_View { get; set; }
        public virtual ICollection<PurchasingInvoiceMng_LoadingPlanSampleDetail_View> PurchasingInvoiceMng_LoadingPlanSampleDetail_View { get; set; }
    }
}
