//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SaleOrderMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleOrderDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleOrderDetail()
        {
            this.SaleOrderDetailExtend = new HashSet<SaleOrderDetailExtend>();
        }
    
        public int SaleOrderDetailID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ClientArticleCode { get; set; }
        public string ClientArticleName { get; set; }
        public string ClientOrderNumber { get; set; }
        public string ClientEANCode { get; set; }
        public string Reference { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> V4ID { get; set; }
    
        public virtual SaleOrder SaleOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderDetailExtend> SaleOrderDetailExtend { get; set; }
    }
}
