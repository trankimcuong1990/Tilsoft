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
    
    public partial class ReportMng_StockList_WarehouseImportDetail_View
    {
        public long RowIndex { get; set; }
        public string KeyID { get; set; }
        public Nullable<int> GoodsID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public string ReceiptNo { get; set; }
        public string ImportTypeNM { get; set; }
        public int ImportQnt { get; set; }
        public int FobWarehouse_ImportedQnt { get; set; }
    }
}
