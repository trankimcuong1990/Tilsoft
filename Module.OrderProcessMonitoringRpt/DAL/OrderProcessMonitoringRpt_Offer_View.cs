//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OrderProcessMonitoringRpt.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProcessMonitoringRpt_Offer_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderProcessMonitoringRpt_Offer_View()
        {
            this.OrderProcessMonitoringRpt_SaleOrder_View = new HashSet<OrderProcessMonitoringRpt_SaleOrder_View>();
        }
    
        public int OfferID { get; set; }
        public string OfferUD { get; set; }
        public Nullable<System.DateTime> OfferDate { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string EmployeeNM { get; set; }
        public string TrackingStatusNM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProcessMonitoringRpt_SaleOrder_View> OrderProcessMonitoringRpt_SaleOrder_View { get; set; }
    }
}