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
    
    public partial class ECommercialInvoiceMng_Booking_View
    {
        public long KeyID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> BookingID { get; set; }
        public string BLNo { get; set; }
        public string ETA { get; set; }
        public string ETA2 { get; set; }
        public string ForwarderInfo { get; set; }
        public string InvoiceNo { get; set; }
        public string FactoryInvoiceNo { get; set; }
        public string ETD { get; set; }
        public Nullable<int> PurchasingInvoiceID { get; set; }
    
        public virtual ECommercialInvoiceMng_ECommercialInvoice_View ECommercialInvoiceMng_ECommercialInvoice_View { get; set; }
    }
}
