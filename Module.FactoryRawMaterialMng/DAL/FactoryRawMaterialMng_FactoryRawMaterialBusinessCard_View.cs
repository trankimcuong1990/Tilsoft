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
    
    public partial class FactoryRawMaterialMng_FactoryRawMaterialBusinessCard_View
    {
        public int FactoryRawMaterialBusinessCardID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FrontFileUD { get; set; }
        public string BehindFileUD { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ControllerID { get; set; }
        public string ControllerName { get; set; }
        public string OtherName { get; set; }
        public string FrontFileLocation { get; set; }
        public string FrontThumbnailLocation { get; set; }
        public string BehindFileLocation { get; set; }
        public string BehindThumbnailLocation { get; set; }
    
        public virtual FactoryRawMaterialMng_FactoryRawMaterial_View FactoryRawMaterialMng_FactoryRawMaterial_View { get; set; }
    }
}
