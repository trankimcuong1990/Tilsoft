//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PackingListMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackingListMng_InitInfo_View
    {
        public PackingListMng_InitInfo_View()
        {
            this.PackingListMng_InitInfoDetail_View = new HashSet<PackingListMng_InitInfoDetail_View>();
            this.PackingListMng_InitInfoSparepartDetail_View = new HashSet<PackingListMng_InitInfoSparepartDetail_View>();
        }
    
        public int PurchasingInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string BLNo { get; set; }
        public Nullable<System.DateTime> ShipedDate { get; set; }
        public string ForwarderNM { get; set; }
        public string PODName { get; set; }
        public string POLName { get; set; }
        public Nullable<int> SupplierID { get; set; }
    
        public virtual ICollection<PackingListMng_InitInfoDetail_View> PackingListMng_InitInfoDetail_View { get; set; }
        public virtual ICollection<PackingListMng_InitInfoSparepartDetail_View> PackingListMng_InitInfoSparepartDetail_View { get; set; }
    }
}
