//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.DraftCommercialInvoiceMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DraftCommercialInvoiceMng_SaleOrderDetailSparepart_View
    {
        public int SaleOrderDetailSparepartID { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Season { get; set; }
    }
}
