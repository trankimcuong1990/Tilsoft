//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.PlanningProductionMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlanningProductionMng_PlanningProductionSearch_View
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<bool> HasBOM { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> FinishDate { get; set; }
        public Nullable<int> WorkOrderStatusID { get; set; }
        public int PlanningProductionID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string ProductArticleCode { get; set; }
        public int BOMID { get; set; }
    }
}
