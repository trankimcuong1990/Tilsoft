//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Framework.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FW_UserFactoryAccess_View
    {
        public int UserFactoryAccessID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<bool> CanAccess { get; set; }
        public Nullable<int> SupplierID { get; set; }
    }
}
