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
    
    public partial class ECommercialInvoiceMng_ECommercialInvoiceSparepartDetailDescription_View
    {
        public int ECommercialInvoiceSparepartDetailDescriptionID { get; set; }
        public Nullable<int> ECommercialInvoiceSparepartDetailID { get; set; }
        public string Description { get; set; }
        public Nullable<int> RowIndex { get; set; }
    
        public virtual ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View ECommercialInvoiceMng_ECommercialInvoiceSparepartDetail_View { get; set; }
    }
}
