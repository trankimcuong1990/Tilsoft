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
    
    public partial class ImageGalleryClient
    {
        public int ImageGalleryClientID { get; set; }
        public Nullable<int> ImageGalleryID { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        public virtual ImageGallery ImageGallery { get; set; }
    }
}