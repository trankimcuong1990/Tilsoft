//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.TransportCostForwarder.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransportCostForwarder_TransportCostForwarder_View
    {
        public TransportCostForwarder_TransportCostForwarder_View()
        {
            this.TransportCostForwarder_TransportCostForwarderItem_View = new HashSet<TransportCostForwarder_TransportCostForwarderItem_View>();
        }
    
        public int TransportCostForwarderID { get; set; }
        public Nullable<int> ForwarderID { get; set; }
        public string TransportInvoiceNumber { get; set; }
        public Nullable<System.DateTime> TransportInvoiceDate { get; set; }
        public Nullable<int> BookingID { get; set; }
        public string Currency { get; set; }
        public string Authorisation { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatorNM { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
        public string ForwarderNM { get; set; }
        public string BookingUD { get; set; }
        public string BLNo { get; set; }
        public string ForwarderUD { get; set; }
        public Nullable<System.DateTime> ETD { get; set; }
        public string CreatorEmpNM { get; set; }
        public string UpdatorEmpNM { get; set; }
    
        public virtual ICollection<TransportCostForwarder_TransportCostForwarderItem_View> TransportCostForwarder_TransportCostForwarderItem_View { get; set; }
    }
}