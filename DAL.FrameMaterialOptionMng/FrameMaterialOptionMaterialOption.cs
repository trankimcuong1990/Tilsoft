//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FrameMaterialOptionMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FrameMaterialOptionMaterialOption
    {
        public int FrameMaterialOptionMaterialOptionID { get; set; }
        public Nullable<int> FrameMaterialOptionID { get; set; }
        public Nullable<int> MaterialOptionID { get; set; }
    
        public virtual FrameMaterialOption FrameMaterialOption { get; set; }
    }
}