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
    
    public partial class ModelMng_ModelCushionOption_View
    {
        public int ModelCushionOptionID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public Nullable<int> CushionID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string CushionUD { get; set; }
        public string CushionNM { get; set; }
        public string UpdatorName { get; set; }
        public string ConfirmerName { get; set; }
        public string Season { get; set; }
        public Nullable<bool> IsStandard { get; set; }
    
        public virtual ModelMng_Model_View ModelMng_Model_View { get; set; }
    }
}
