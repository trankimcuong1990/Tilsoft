//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CPLoadingPlan.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CP_LoadingPlanMng_LoadingPlanSearchResult_View
    {
        public int LoadingPlanID { get; set; }
        public string BLNo { get; set; }
        public Nullable<System.DateTime> ETD { get; set; }
        public Nullable<System.DateTime> ETA { get; set; }
        public string LoadingPlanUD { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public Nullable<bool> IsLoaded { get; set; }
        public Nullable<System.DateTime> LoadingDate { get; set; }
        public Nullable<bool> IsShipped { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public Nullable<int> ContainerTypeID { get; set; }
    }
}
