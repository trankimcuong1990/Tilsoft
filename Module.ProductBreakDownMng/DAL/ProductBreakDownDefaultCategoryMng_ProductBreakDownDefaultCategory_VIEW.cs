//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductBreakDownMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductBreakDownDefaultCategoryMng_ProductBreakDownDefaultCategory_View
    {
        public int ProductBreakDownDefaultCategoryID { get; set; }
        public string ProductBreakDownDefaultCategoryNM { get; set; }
        public Nullable<int> ProductBreakDownCalculationTypeID { get; set; }
        public string ProductBreakDownCalculationTypeNM { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string InformationCreatorNM { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string InformationUpdatorNM { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> OptionTotalID { get; set; }
        public string OptionTotalNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<decimal> QuantityPercent { get; set; }
        public Nullable<int> OptionToGetPriceID { get; set; }
        public string OptionToGetPriceNM { get; set; }
    }
}
