//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.FactorySalesQuotationMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactorySaleQuotationMng_FactorySaleQuotationSearchResult_View
    {
        public int FactorySaleQuotationID { get; set; }
        public string FactorySaleQuotationUD { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialContactPersonNM { get; set; }
        public string FactoryRawMaterialDocumentRefNo { get; set; }
        public string Currency { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public Nullable<int> FactorySaleUserID { get; set; }
        public string ShippingAddress { get; set; }
        public Nullable<int> FactoryShippingMethodID { get; set; }
        public string BillingAddress { get; set; }
        public Nullable<int> FactoryPaymentTermID { get; set; }
        public Nullable<System.DateTime> ExpectedPaidDate { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> ValidUntil { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string FactorySaleQuotationStatusNM { get; set; }
        public Nullable<int> FactorySaleQuotationStatusID { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdaterName { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public string Confirmer { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
