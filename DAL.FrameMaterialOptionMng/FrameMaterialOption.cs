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
    
    public partial class FrameMaterialOption
    {
        public FrameMaterialOption()
        {
            this.FrameMaterialOptionProductGroup = new HashSet<FrameMaterialOptionProductGroup>();
            this.FrameMaterialOptionMaterialOption = new HashSet<FrameMaterialOptionMaterialOption>();
        }
    
        public int FrameMaterialOptionID { get; set; }
        public string FrameMaterialOptionUD { get; set; }
        public string FrameMaterialOptionNM { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string Remark { get; set; }
    
        public virtual ICollection<FrameMaterialOptionProductGroup> FrameMaterialOptionProductGroup { get; set; }
        public virtual ICollection<FrameMaterialOptionMaterialOption> FrameMaterialOptionMaterialOption { get; set; }
    }
}