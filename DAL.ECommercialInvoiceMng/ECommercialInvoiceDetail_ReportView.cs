//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ECommercialInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ECommercialInvoiceDetail_ReportView
    {
        public ECommercialInvoiceDetail_ReportView()
        {
            this.ECommercialInvoiceDetailDescription_ReportView = new HashSet<ECommercialInvoiceDetailDescription_ReportView>();
        }
    
        public int ECommercialInvoiceDetailID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerType { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ContInfo { get; set; }
        public string ContTypeInfo { get; set; }
    
        public virtual ECommercialInvoice_ReportView ECommercialInvoice_ReportView { get; set; }
        public virtual ICollection<ECommercialInvoiceDetailDescription_ReportView> ECommercialInvoiceDetailDescription_ReportView { get; set; }
    }
}
