//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryQuotation2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuotationOfferDetail
    {
        public int QuotationOfferDetailID { get; set; }
        public Nullable<int> QuotationOfferID { get; set; }
        public Nullable<int> QuotationDetailID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Remark { get; set; }
    
        public virtual QuotationDetail QuotationDetail { get; set; }
        public virtual QuotationOffer QuotationOffer { get; set; }
    }
}
