//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientPaymentMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientPayment
    {
        public ClientPayment()
        {
            this.ClientPaymentDetail = new HashSet<ClientPaymentDetail>();
        }
    
        public int ClientPaymentID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<bool> IsLCReceived { get; set; }
    
        public virtual ICollection<ClientPaymentDetail> ClientPaymentDetail { get; set; }
    }
}
