//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.CostInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostInvoiceMng_CostInvoiceDetail_View
    {
        public CostInvoiceMng_CostInvoiceDetail_View()
        {
            this.CostInvoiceMng_CostInvoiceDetailExtend_View = new HashSet<CostInvoiceMng_CostInvoiceDetailExtend_View>();
        }
    
        public int CostInvoiceDetailID { get; set; }
        public Nullable<int> CostInvoiceID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public string ContainerNo { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string CostType { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        public virtual CostInvoiceMng_CostInvoice_View CostInvoiceMng_CostInvoice_View { get; set; }
        public virtual ICollection<CostInvoiceMng_CostInvoiceDetailExtend_View> CostInvoiceMng_CostInvoiceDetailExtend_View { get; set; }
    }
}