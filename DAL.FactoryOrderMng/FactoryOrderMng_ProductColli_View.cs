//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryOrderMng_ProductColli_View
    {
        public long RowIndex { get; set; }
        public int FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> ProductSetEANCodeID { get; set; }
        public Nullable<int> ProductColliID { get; set; }
        public string ProductEANCode { get; set; }
        public string ColliEANCode { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
}
