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
    
    public partial class FactoryMng2_FactoryManager_View
    {
        public int FactoryManagerID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<bool> PIC { get; set; }
        public Nullable<bool> IsReceivePriceRequest { get; set; }
        public string EmployeeNM { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual FactoryMng2_Factory_View FactoryMng2_Factory_View { get; set; }
        public virtual FactoryMng2_FactorySearchResult_View FactoryMng2_FactorySearchResult_View { get; set; }
    }
}
