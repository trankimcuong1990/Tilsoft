//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FrameMaterialProfileMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FrameMaterialProfileMng_FrameMaterialProfileFactory_View
    {
        public int FrameMaterialProfileFactoryID { get; set; }
        public Nullable<int> FrameMaterialProfileID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
    
        public virtual FrameMaterialProfileMng_FrameMaterialProfile_View FrameMaterialProfileMng_FrameMaterialProfile_View { get; set; }
    }
}