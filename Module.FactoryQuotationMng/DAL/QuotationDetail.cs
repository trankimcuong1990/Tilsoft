//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactoryQuotationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuotationDetail
    {
        public QuotationDetail()
        {
            this.QuotationOfferDetail = new HashSet<QuotationOfferDetail>();
        }
    
        public int QuotationDetailID { get; set; }
        public Nullable<int> QuotationID { get; set; }
        public Nullable<int> FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryOrderSparepartDetailID { get; set; }
        public string PriceDifferenceCode { get; set; }
        public Nullable<decimal> PriceDifferenceRate { get; set; }
        public Nullable<decimal> BreakDownPrice { get; set; }
        public Nullable<decimal> PurchasingPrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> TargetPrice { get; set; }
        public string Remark { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> StatusUpdatedBy { get; set; }
        public Nullable<System.DateTime> StatusUpdatedDate { get; set; }
        public Nullable<decimal> OldPrice { get; set; }
        public Nullable<System.DateTime> ProductAttributeUpdatedDate { get; set; }
        public string OldProductAttribute { get; set; }
        public Nullable<System.DateTime> PriceUpdatedDate { get; set; }
        public Nullable<int> PriceUpdatedBy { get; set; }
        public Nullable<int> LastOfferDirectionID { get; set; }
    
        public virtual Quotation Quotation { get; set; }
        public virtual ICollection<QuotationOfferDetail> QuotationOfferDetail { get; set; }
    }
}
