//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PLCMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class PLCMng_PLCImage_View
    {
        public int PLCImageID { get; set; }
        public Nullable<int> PLCID { get; set; }
        public Nullable<int> ImageTypeID { get; set; }
        public string ImageFile { get; set; }
        public string PLCImageTypeNM { get; set; }
        public string ImageFile_DisplayUrl { get; set; }
        public string ImageFile_FullSizeUrl { get; set; }
    
        public virtual PLCMng_PLC_View PLCMng_PLC_View { get; set; }
    }
}