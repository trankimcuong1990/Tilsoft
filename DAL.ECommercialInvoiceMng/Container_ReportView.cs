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
    
    public partial class Container_ReportView
    {
        public long KeyID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public string ContainerNo { get; set; }
        public string ContainerType { get; set; }
        public string SealNo { get; set; }
        public string ContInfo { get; set; }
        public string ContTypeInfo { get; set; }
    
        public virtual ECommercialInvoice_ReportView ECommercialInvoice_ReportView { get; set; }
    }
}