//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ProductionSummaryRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionSummaryRpt_ProductionSummaryVirtual_View
    {
        public Nullable<int> ProductID { get; set; }
        public string FactoryUD { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string WorkOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qnt40HC { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public Nullable<int> WorkOrderQnt { get; set; }
        public long PrimaryID { get; set; }
        public Nullable<decimal> Order40HC { get; set; }
        public Nullable<decimal> Pro40HC { get; set; }
        public Nullable<int> WorkOrderStatusID { get; set; }
        public Nullable<int> PreOrderWorkOrderID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public string PreOrderWorkOrderUD { get; set; }
    }
}