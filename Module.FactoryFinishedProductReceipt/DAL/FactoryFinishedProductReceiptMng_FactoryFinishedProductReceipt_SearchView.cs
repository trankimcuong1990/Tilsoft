//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryFinishedProductReceipt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_SearchView
    {
        public int FactoryFinishedProductReceiptID { get; set; }
        public Nullable<int> ReceiptTypeID { get; set; }
        public string ReceiptTypeText { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ProductTypeText { get; set; }
        public string ReceiptNo { get; set; }
        public string Season { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<int> ImportFromTeamID { get; set; }
        public string ImportFromTeamNM { get; set; }
        public Nullable<int> ExportToTeamID { get; set; }
        public string ExportToTeamNM { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string FactoryStepNM { get; set; }
        public Nullable<bool> IsOutsource { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
        public Nullable<int> CreatorID { get; set; }
        public Nullable<int> UpdatorID { get; set; }
    }
}
