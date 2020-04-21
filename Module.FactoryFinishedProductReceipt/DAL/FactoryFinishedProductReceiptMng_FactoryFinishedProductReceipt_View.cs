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
    
    public partial class FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_View
    {
        public FactoryFinishedProductReceiptMng_FactoryFinishedProductReceipt_View()
        {
            this.FactoryFinishedProductReceiptMng_FactoryFinishedProductReceiptDetail_View = new HashSet<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceiptDetail_View>();
        }
    
        public int FactoryFinishedProductReceiptID { get; set; }
        public Nullable<int> ReceiptTypeID { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ReceiptNo { get; set; }
        public string Season { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<int> ImportFromTeamID { get; set; }
        public Nullable<int> ExportToTeamID { get; set; }
        public Nullable<int> FactoryGoodsProcedureID { get; set; }
        public Nullable<int> FactoryStepID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsOutsource { get; set; }
        public string ReceiptTypeText { get; set; }
        public string ProductTypeText { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string FactoryTeamNM { get; set; }
        public string FactoryStepNM { get; set; }
        public string FactoryGoodsProcedureNM { get; set; }
        public Nullable<int> BaseOnTypeID { get; set; }
        public Nullable<int> InProgressOrBuyDirectlyID { get; set; }
        public string BaseOnTypeText { get; set; }
        public string InProgressOrBuyDirectlyText { get; set; }
    
        public virtual ICollection<FactoryFinishedProductReceiptMng_FactoryFinishedProductReceiptDetail_View> FactoryFinishedProductReceiptMng_FactoryFinishedProductReceiptDetail_View { get; set; }
    }
}
