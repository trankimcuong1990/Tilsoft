//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ImageGalleryMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageGalleryVersion
    {
        public int ImageGalleryVersionID { get; set; }
        public Nullable<int> ImageGalleryID { get; set; }
        public Nullable<int> Version { get; set; }
        public string FileUD { get; set; }
        public string Description { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    
        public virtual ImageGallery ImageGallery { get; set; }
    }
}
