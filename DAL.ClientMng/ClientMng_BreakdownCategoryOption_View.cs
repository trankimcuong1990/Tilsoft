//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientMng_BreakdownCategoryOption_View
    {
        public int BreakdownCategoryOptionID { get; set; }
        public Nullable<int> BreakdownID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> OfferLineID { get; set; }
        public Nullable<int> BreakdownCategoryID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string BreakdownCategoryNM { get; set; }
        public Nullable<bool> IsDefaultOption { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> CushionTypeID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public string PackagingMethodNM { get; set; }
        public string Description { get; set; }
        public Nullable<int> ClientSpecialPackagingMethodID { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public int LoadAbilityAVF { get; set; }
        public int LoadAbilityAVT { get; set; }
    }
}