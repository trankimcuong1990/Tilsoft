//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryMng2.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryMng2_FactoryGallery_View
    {
        public int FactoryGalleryID { get; set; }
        public string FactoryGalleryUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryGalleryFile { get; set; }
        public string FactoryGalleryThumbnail { get; set; }
        public long RowIndex { get; set; }
    
        public virtual FactoryMng2_Factory_View FactoryMng2_Factory_View { get; set; }
    }
}