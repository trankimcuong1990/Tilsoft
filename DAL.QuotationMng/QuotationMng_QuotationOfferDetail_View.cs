//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.QuotationMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuotationMng_QuotationOfferDetail_View
    {
        public int QuotationOfferDetailID { get; set; }
        public Nullable<int> QuotationOfferID { get; set; }
        public Nullable<int> QuotationDetailID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Remark { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public string ClientUD { get; set; }
        public string FactoryOrderUD { get; set; }
    
        public virtual QuotationMng_QuotationOffer_View QuotationMng_QuotationOffer_View { get; set; }
        public virtual QuotationMng_QuotationDetail_View QuotationMng_QuotationDetail_View { get; set; }
    }
}
