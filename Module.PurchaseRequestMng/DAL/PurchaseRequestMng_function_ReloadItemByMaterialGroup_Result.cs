//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PurchaseRequestMng.DAL
{
    using System;
    
    public partial class PurchaseRequestMng_function_ReloadItemByMaterialGroup_Result
    {
        public Nullable<int> ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public Nullable<decimal> RequestQnt { get; set; }
        public decimal OrderQnt { get; set; }
        public decimal StockQnt { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<int> ProductionItemGroupID { get; set; }
        public Nullable<int> ProductionItemTypeID { get; set; }
    }
}