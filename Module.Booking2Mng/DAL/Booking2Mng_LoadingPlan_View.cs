//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Booking2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking2Mng_LoadingPlan_View
    {
        public int LoadingPlanID { get; set; }
        public string LoadingPlanUD { get; set; }
        public Nullable<int> BookingID { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string SealNo { get; set; }
        public bool IsShipped { get; set; }
        public bool IsLoaded { get; set; }
        public bool IsConfirmed { get; set; }
        public int IsUploadingImageFinish { get; set; }
    
        public virtual Booking2Mng_Booking_View Booking2Mng_Booking_View { get; set; }
    }
}
