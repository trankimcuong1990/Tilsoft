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
    
    public partial class QuotationMng_Quotation_View
    {
        public QuotationMng_Quotation_View()
        {
            this.QuotationMng_QuotationDetail_View = new HashSet<QuotationMng_QuotationDetail_View>();
            this.QuotationMng_QuotationOffer_View = new HashSet<QuotationMng_QuotationOffer_View>();
        }
    
        public int QuotationID { get; set; }
        public string QuotationUD { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string Season { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string UpdatorName { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public string UpdatorName2 { get; set; }
    
        public virtual ICollection<QuotationMng_QuotationDetail_View> QuotationMng_QuotationDetail_View { get; set; }
        public virtual ICollection<QuotationMng_QuotationOffer_View> QuotationMng_QuotationOffer_View { get; set; }
    }
}
