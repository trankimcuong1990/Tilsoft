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
    
    public partial class ProductBreakDownPAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductBreakDownPAL()
        {
            this.ProductBreakDownCategoryPAL = new HashSet<ProductBreakDownCategoryPAL>();
        }
    
        public int ProductBreakDownID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string ItemSize { get; set; }
        public string CartonSize { get; set; }
        public string FrameDescription { get; set; }
        public string CushionDescription { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<decimal> SeasonSpec { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> MonthPrice { get; set; }
        public Nullable<int> YearPrice { get; set; }
        public Nullable<System.DateTime> PriceDate { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBreakDownCategoryPAL> ProductBreakDownCategoryPAL { get; set; }
        public virtual Product Product { get; set; }
    }
}