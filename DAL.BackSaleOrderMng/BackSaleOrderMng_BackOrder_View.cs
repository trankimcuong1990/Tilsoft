//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BackSaleOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class BackSaleOrderMng_BackOrder_View
    {
        public BackSaleOrderMng_BackOrder_View()
        {
            this.BackSaleOrderMng_BackOrderDetail_View = new HashSet<BackSaleOrderMng_BackOrderDetail_View>();
        }
    
        public int BackOrderID { get; set; }
        public Nullable<int> OfferID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public string Season { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> VATPercent { get; set; }
        public string Currency { get; set; }
        public string OrderType { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
    
        public virtual ICollection<BackSaleOrderMng_BackOrderDetail_View> BackSaleOrderMng_BackOrderDetail_View { get; set; }
    }
}