//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.LoadingPlanMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoadingPlanSparepartDetail
    {
        public LoadingPlanSparepartDetail()
        {
            this.PurchasingInvoiceSparepartDetail = new HashSet<PurchasingInvoiceSparepartDetail>();
        }
    
        public int LoadingPlanSparepartDetailID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<int> FactoryOrderSparepartDetailID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Remark { get; set; }
        public string Unit { get; set; }
        public Nullable<int> PKGs { get; set; }
        public Nullable<decimal> NettWeight { get; set; }
        public Nullable<decimal> KGs { get; set; }
        public Nullable<decimal> CBMs { get; set; }
        public string HSCode { get; set; }
    
        public virtual LoadingPlan LoadingPlan { get; set; }
        public virtual ICollection<PurchasingInvoiceSparepartDetail> PurchasingInvoiceSparepartDetail { get; set; }
    }
}
