//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.LabelingPackaging.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LabelingPackagingMng_LabelingPackagingFileMonitor_View
    {
        public int LPFileMonitorID { get; set; }
        public Nullable<int> LabelingPackagingID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<bool> HangTagFileStatus { get; set; }
        public Nullable<bool> BoxShippingMarkFileStatus { get; set; }
        public Nullable<bool> BrassLabelFileStatus { get; set; }
        public Nullable<bool> AIFileStatus { get; set; }
        public Nullable<bool> WashCushionLabelFileStatus { get; set; }
    
        public virtual LabelingPackagingMng_LabelingPackaging_View LabelingPackagingMng_LabelingPackaging_View { get; set; }
    }
}