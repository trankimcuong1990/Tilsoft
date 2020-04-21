//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.MaterialOptionMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialOption
    {
        public MaterialOption()
        {
            this.MaterialOptionProductGroup = new HashSet<MaterialOptionProductGroup>();
            this.MaterialOptionTestReport = new HashSet<MaterialOptionTestReport>();
        }
    
        public int MaterialOptionID { get; set; }
        public string MaterialOptionUD { get; set; }
        public string MaterialOptionNM { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
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
    
        public virtual ICollection<MaterialOptionProductGroup> MaterialOptionProductGroup { get; set; }
        public virtual ICollection<MaterialOptionTestReport> MaterialOptionTestReport { get; set; }
    }
}
