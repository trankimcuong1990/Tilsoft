//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductTestingMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductTestingMng_ProductTesting_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductTestingMng_ProductTesting_View()
        {
            this.ProductTestingMng_ProductTestFile_View = new HashSet<ProductTestingMng_ProductTestFile_View>();
            this.ProductTestingMng_ProductTestingStandard_View = new HashSet<ProductTestingMng_ProductTestingStandard_View>();
        }
    
        public int ProductTestID { get; set; }
        public string ProductTestUD { get; set; }
        public Nullable<int> ProductTestStandardID { get; set; }
        public Nullable<System.DateTime> TestDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string UpdatorName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTestingMng_ProductTestFile_View> ProductTestingMng_ProductTestFile_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTestingMng_ProductTestingStandard_View> ProductTestingMng_ProductTestingStandard_View { get; set; }
    }
}
