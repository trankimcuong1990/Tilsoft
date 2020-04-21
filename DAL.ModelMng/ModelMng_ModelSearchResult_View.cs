//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ModelMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModelMng_ModelSearchResult_View
    {
        public int ModelID { get; set; }
        public Nullable<bool> IsStandard { get; set; }
        public string Season { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public string RangeName { get; set; }
        public string Collection { get; set; }
        public Nullable<int> CataloguePageNo { get; set; }
        public string ProductTypeNM { get; set; }
        public string ProductGroupNM { get; set; }
        public string ManufacturerCountryNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ModelStatusNM { get; set; }
        public string ProductCategoryNM { get; set; }
        public string FactoryUD { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public Nullable<bool> HasCushion { get; set; }
        public Nullable<bool> HasFrameMaterial { get; set; }
        public Nullable<bool> HasSubMaterial { get; set; }
        public Nullable<int> ManufacturerCountryID { get; set; }
        public Nullable<int> ModelStatusID { get; set; }
        public Nullable<int> ProductCategoryID { get; set; }
        public Nullable<bool> IsExcludedInNotification { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName2 { get; set; }
        public Nullable<bool> IsArchived { get; set; }
        public string CreatorName { get; set; }
        public string CreatorName2 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsDefaultConfirmed { get; set; }
    }
}