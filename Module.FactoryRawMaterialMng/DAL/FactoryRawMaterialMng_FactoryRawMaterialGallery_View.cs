//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryRawMaterialMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryRawMaterialMng_FactoryRawMaterialGallery_View
    {
        public int FactoryRawMaterialGalleryID { get; set; }
        public string FactoryRawMaterialGalleryUD { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FactoryGalleryFile { get; set; }
        public string FactoryGalleryThumbnail { get; set; }
        public long RowIndex { get; set; }
    
        public virtual FactoryRawMaterialMng_FactoryRawMaterial_View FactoryRawMaterialMng_FactoryRawMaterial_View { get; set; }
    }
}
