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
    
    public partial class ECommercialInvoiceContainerTransport_ReportView
    {
        public int ECommercialInvoiceContainerTransportID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> ClientCostItemID { get; set; }
        public Nullable<int> LoadingPlanID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string ClientCostItemNM { get; set; }
        public string ContainerNo { get; set; }
        public Nullable<int> ClientCostTypeID { get; set; }
        public string ClientCostTypeNM { get; set; }
        public string ContainerTypeNM { get; set; }
        public Nullable<int> ContainerTypeID { get; set; }
        public string ClientOrderNumber { get; set; }
        public Nullable<int> GroupIndex { get; set; }
    
        public virtual ECommercialInvoice_ReportView ECommercialInvoice_ReportView { get; set; }
    }
}