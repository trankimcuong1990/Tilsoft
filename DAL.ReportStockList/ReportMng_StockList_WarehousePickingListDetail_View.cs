//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ReportStockList
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportMng_StockList_WarehousePickingListDetail_View
    {
        public long RowIndex { get; set; }
        public string KeyID { get; set; }
        public Nullable<int> GoodsID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public string ReceiptNo { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string OrderType { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int PickedQnt { get; set; }
    }
}