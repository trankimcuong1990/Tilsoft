//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ECommercialInvoiceMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class ECommercialInvoiceMng_ReturnSparepart_View
    {
        public int ECommercialInvoiceSparepartDetailID { get; set; }
        public Nullable<int> ECommercialInvoiceID { get; set; }
        public Nullable<int> SparepartID { get; set; }
        public Nullable<int> ProductStatusID { get; set; }
        public Nullable<int> CreditNoteQnt { get; set; }
        public Nullable<int> RemaingReturnQnt { get; set; }
        public Nullable<int> ReturnQnt { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string BLNo { get; set; }
        public string ContainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerTypeNM { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ProductStatusNM { get; set; }
    }
}
