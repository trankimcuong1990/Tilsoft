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
    
    public partial class ModelDefaultOptionMng_ModelDefaultOption_View
    {
        public int ModelDefaultOptionID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> MaterialID { get; set; }
        public Nullable<int> MaterialTypeID { get; set; }
        public Nullable<int> MaterialColorID { get; set; }
        public Nullable<int> FrameMaterialID { get; set; }
        public Nullable<int> FrameMaterialColorID { get; set; }
        public Nullable<int> SubMaterialID { get; set; }
        public Nullable<int> SubMaterialColorID { get; set; }
        public Nullable<int> BackCushionID { get; set; }
        public Nullable<int> SeatCushionID { get; set; }
        public Nullable<int> CushionColorID { get; set; }
        public Nullable<int> FSCTypeID { get; set; }
        public Nullable<int> FSCPercentID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> PackagingMethodID { get; set; }
        public Nullable<int> ClientSpecialPackagingMethodID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string PackagingText { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string MaterialText { get; set; }
        public string FrameText { get; set; }
        public string SubMaterialText { get; set; }
        public string CushionText { get; set; }
        public string FSCText { get; set; }
        public string MaterialThumbnailLocation { get; set; }
        public string FrameMaterialThumbnailLocation { get; set; }
        public string SubMaterialColorThumbnailLocation { get; set; }
        public string CushionColorThumbnailLocation { get; set; }
        public string BackCushionThumbnailLocation { get; set; }
        public string SeatCushionThumbnailLocation { get; set; }
    
        public virtual ModelMng_Model_View ModelMng_Model_View { get; set; }
    }
}
