//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.WorkOrder.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkOrderMng_WorkOrder_SearchView
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string OpSequenceNM { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public string ModelUD { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsCreatedBOM { get; set; }
        public Nullable<System.DateTime> CreatedDateBOM { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ConfirmerName { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public string UpdatorName { get; set; }
        public string SetStatustorName { get; set; }
        public Nullable<System.DateTime> SetStatusDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> WorkOrderStatusID { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public Nullable<bool> HasTransaction { get; set; }
        public Nullable<int> WorkOrderTypeID { get; set; }
        public string PreOrderDescription { get; set; }
        public string WorkOrderPreOrderUD { get; set; }
        public Nullable<bool> HasPurchaseRequest { get; set; }
        public Nullable<bool> IsCreatePRGroup { get; set; }
    }
}
