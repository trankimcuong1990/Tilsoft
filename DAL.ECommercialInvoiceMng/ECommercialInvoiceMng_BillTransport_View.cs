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
    
    public partial class ECommercialInvoiceMng_BillTransport_View
    {
        public ECommercialInvoiceMng_BillTransport_View()
        {
            this.ECommercialInvoiceMng_ContainerTransport_View = new HashSet<ECommercialInvoiceMng_ContainerTransport_View>();
        }
    
        public int BookingID { get; set; }
        public string BLNo { get; set; }
        public string Season { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string PoLName { get; set; }
        public Nullable<int> POLID { get; set; }
    
        public virtual ICollection<ECommercialInvoiceMng_ContainerTransport_View> ECommercialInvoiceMng_ContainerTransport_View { get; set; }
    }
}
