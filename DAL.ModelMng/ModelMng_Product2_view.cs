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
    
    public partial class ModelMng_Product2_view
    {
        public int ProductID { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    
        public virtual ModelMng_Model_View ModelMng_Model_View { get; set; }
    }
}
