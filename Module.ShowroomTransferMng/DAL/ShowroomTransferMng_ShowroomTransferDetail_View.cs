//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.ShowroomTransferMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShowroomTransferMng_ShowroomTransferDetail_View
    {
        public int ShowroomTransferDetailID { get; set; }
        public Nullable<int> ProductionItemID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> QntBarcode { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public Nullable<int> ShowroomTransferID { get; set; }
        public Nullable<int> FactoryWarehousePalletID { get; set; }
        public string FactoryWarehousePalletUD { get; set; }
        public string FactoryWarehousePalletNM { get; set; }
        public Nullable<int> FactoryWarehouseToPalletID { get; set; }
        public string FactoryWarehouseToPalletUD { get; set; }
        public string FactoryWarehouseToPalletNM { get; set; }
    
        public virtual ShowroomTransferMng_ShowroomTransfer_View ShowroomTransferMng_ShowroomTransfer_View { get; set; }
    }
}
