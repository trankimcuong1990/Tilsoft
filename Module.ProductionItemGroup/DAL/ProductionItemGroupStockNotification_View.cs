//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionItemGroup.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionItemGroupStockNotification_View
    {
        public int ProductionItemGroupStockNotificationID { get; set; }
        public Nullable<int> ProductionItemGroupID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Remark { get; set; }
        public string EmployeeNM { get; set; }
    
        public virtual ProductionItemMng_ProductionItemGroup_View ProductionItemMng_ProductionItemGroup_View { get; set; }
    }
}