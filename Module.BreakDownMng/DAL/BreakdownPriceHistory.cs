//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.BreakDownMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BreakdownPriceHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BreakdownPriceHistory()
        {
            this.BreakdownPriceHistoryDetail = new HashSet<BreakdownPriceHistoryDetail>();
        }
    
        public int BreakdownPriceHistoryID { get; set; }
        public string BreakdownPriceHistoryUD { get; set; }
        public string BreakdownPriceHistoryNM { get; set; }
        public string ArticleCode { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> BreakdownID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Breakdown Breakdown { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BreakdownPriceHistoryDetail> BreakdownPriceHistoryDetail { get; set; }
    }
}