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
    
    public partial class MaterialOptionMng_MaterialOptionSearchResult_View
    {
        public MaterialOptionMng_MaterialOptionSearchResult_View()
        {
            this.MaterialOptionMng_MaterialOptionTestReport_View = new HashSet<MaterialOptionMng_MaterialOptionTestReport_View>();
            this.MaterialOptionMng_MaterialTesting_View = new HashSet<MaterialOptionMng_MaterialTesting_View>();
        }
    
        public int MaterialOptionID { get; set; }
        public string MaterialOptionUD { get; set; }
        public string MaterialOptionNM { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public string ProductGroupNM { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Remark { get; set; }
    
        public virtual ICollection<MaterialOptionMng_MaterialOptionTestReport_View> MaterialOptionMng_MaterialOptionTestReport_View { get; set; }
        public virtual ICollection<MaterialOptionMng_MaterialTesting_View> MaterialOptionMng_MaterialTesting_View { get; set; }
    }
}
