//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.CushionColorMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class CushionColor
    {
        public CushionColor()
        {
            this.CushionColorProductGroup = new HashSet<CushionColorProductGroup>();
            this.CushionColorTestReport = new HashSet<CushionColorTestReport>();
        }
    
        public int CushionColorID { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }
        public string ImageFile { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<int> CushionTypeID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
        public string TestReportFile1 { get; set; }
        public string TestReportFile2 { get; set; }
        public string TestReportFile3 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> CushionPriceGroupID { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual ICollection<CushionColorProductGroup> CushionColorProductGroup { get; set; }
        public virtual ICollection<CushionColorTestReport> CushionColorTestReport { get; set; }
    }
}
