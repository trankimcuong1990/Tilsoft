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
    
    public partial class FactoryOrderColliDetail
    {
        public int FactoryOrderColliDetailID { get; set; }
        public Nullable<int> ProductColliID { get; set; }
    
        public virtual FactoryOrderDetail FactoryOrderDetail { get; set; }
    }
}
