//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.WarehouseCIMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseCIMng_WarehouseCI_SearchView
    {
        public int WarehouseCIID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> IssuedDate { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string OrderNo { get; set; }
        public string RefNo { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> ExRate { get; set; }
        public Nullable<int> LedgerAccountID { get; set; }
        public Nullable<int> WarehouseID { get; set; }
        public Nullable<decimal> VATRate { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> AccountNo { get; set; }
        public string WarehouseNM { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
    }
}
