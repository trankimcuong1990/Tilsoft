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
    
    public partial class PackingListMng_PackingListDetailExtend_View
    {
        public int PackingListDetailExtendID { get; set; }
        public Nullable<int> PackingListID { get; set; }
        public string Unit { get; set; }
        public Nullable<int> QuantityShipped { get; set; }
        public Nullable<int> PKGs { get; set; }
        public Nullable<decimal> NettWeight { get; set; }
        public Nullable<decimal> KGs { get; set; }
        public Nullable<decimal> CBMs { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerType { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
    
        public virtual PackingListMng_PackingList_View PackingListMng_PackingList_View { get; set; }
    }
}
