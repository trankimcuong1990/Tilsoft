//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Sample3Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sample3Mng_SampleOrderOverview_Monitor_View
    {
        public int SampleMonitorID { get; set; }
        public Nullable<int> SampleOrderID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> SampleMonitorGroupID { get; set; }
        public string FullName { get; set; }
        public string InternalCompanyNM { get; set; }
    
        public virtual Sample3Mng_SampleOrderOverview_Order_View Sample3Mng_SampleOrderOverview_Order_View { get; set; }
    }
}
