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
    
    public partial class ECommercialInvoiceMng_Client_View
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ClientAddress { get; set; }
        public string VATNo { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public string DeliveryTermNM { get; set; }
        public string PaymentTermNM { get; set; }
    }
}