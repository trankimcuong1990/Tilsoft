//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DeliveryNote2.DAL
{
    using System;
    
    public partial class DeliveryNoteMng_Function_SupportProductionItem_View_Result
    {
        public Nullable<int> ProductionItemTypeID { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public Nullable<int> DefaultFactoryWarehouseID { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public decimal StockQnt { get; set; }
        public decimal TotalDelivery { get; set; }
        public decimal UnitPrice { get; set; }
        public Nullable<int> BranchID { get; set; }
        public int ProductionItemID { get; set; }
        public Nullable<int> UnitID { get; set; }
    }
}