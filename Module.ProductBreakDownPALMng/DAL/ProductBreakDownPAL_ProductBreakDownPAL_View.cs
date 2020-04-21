//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductBreakDownPALMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductBreakDownPAL_ProductBreakDownPAL_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductBreakDownPAL_ProductBreakDownPAL_View()
        {
            this.ProductBreakDownPAL_ProductBreakDownCategoryAmountPAL_View = new HashSet<ProductBreakDownPAL_ProductBreakDownCategoryAmountPAL_View>();
            this.ProductBreakDownPAL_ProductBreakDownCategoryPAL_View = new HashSet<ProductBreakDownPAL_ProductBreakDownCategoryPAL_View>();
            this.ProductBreakDownPAL_ProductBreakDownCategoryPercentPAL_View = new HashSet<ProductBreakDownPAL_ProductBreakDownCategoryPercentPAL_View>();
        }
    
        public int ProductBreakDownID { get; set; }
        public string ItemSize { get; set; }
        public string CartonSize { get; set; }
        public string FrameDescription { get; set; }
        public string CushionDescription { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string ArticleDescription { get; set; }
        public string SampleProductUD { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string ConfirmerName { get; set; }
        public Nullable<decimal> SeasonSpec { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public Nullable<System.DateTime> PriceDate { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public string CurrencyNM { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownPAL_ProductBreakDownCategoryAmountPAL_View> ProductBreakDownPAL_ProductBreakDownCategoryAmountPAL_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownPAL_ProductBreakDownCategoryPAL_View> ProductBreakDownPAL_ProductBreakDownCategoryPAL_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownPAL_ProductBreakDownCategoryPercentPAL_View> ProductBreakDownPAL_ProductBreakDownCategoryPercentPAL_View { get; set; }
    }
}
