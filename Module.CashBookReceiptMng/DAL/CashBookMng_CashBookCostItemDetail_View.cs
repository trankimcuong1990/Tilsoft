//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.CashBookReceiptMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CashBookMng_CashBookCostItemDetail_View
    {
        public int CashBookCostItemDetailID { get; set; }
        public Nullable<int> CashBookCostItemID { get; set; }
        public string CostItemNM { get; set; }
        public string CostItemDetailNM { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual CashBookMng_CashBookCostItem_View CashBookMng_CashBookCostItem_View { get; set; }
    }
}