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
    
    public partial class FactorySaleQuotationMng_FactorySaleQuotation_View
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FactorySaleQuotationMng_FactorySaleQuotation_View()
        {
            this.FactorySaleQuotationMng_FactorySaleQuotationDetail_View = new HashSet<FactorySaleQuotationMng_FactorySaleQuotationDetail_View>();
            this.FactorySaleQuotationMng_FactorySaleQuotationFactorySaleQuotationAttachedFile_View = new HashSet<FactorySaleQuotationMng_FactorySaleQuotationFactorySaleQuotationAttachedFile_View>();
        }
    
        public int FactorySaleQuotationID { get; set; }
        public string FactorySaleQuotationUD { get; set; }
        public Nullable<int> FactorySaleQuotationStatusID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FactoryRawMaterialContactPersonNM { get; set; }
        public string FactoryRawMaterialDocumentRefNo { get; set; }
        public string FactoryRawMaterialUD { get; set; }
        public string Currency { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public Nullable<System.DateTime> ValidUntil { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<int> FactorySaleUserID { get; set; }
        public string Remark { get; set; }
        public string ShippingAddress { get; set; }
        public Nullable<int> FactoryShippingMethodID { get; set; }
        public string BillingAddress { get; set; }
        public Nullable<int> FactoryPaymentTermID { get; set; }
        public Nullable<System.DateTime> ExpectedPaidDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string FactoryRawMaterialOfficialNM { get; set; }
        public string EmployeeNM { get; set; }
        public string FactoryRawMaterialShortNM { get; set; }
        public string FactoryShippingMethodNM { get; set; }
        public string FactoryPaymentTermNM { get; set; }
        public string FactorySaleQuotationStatusNM { get; set; }
        public Nullable<int> SupplierContactQuickInfoId { get; set; }
        public string FullName { get; set; }
        public string Creator { get; set; }
        public string Approver { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactorySaleQuotationMng_FactorySaleQuotationDetail_View> FactorySaleQuotationMng_FactorySaleQuotationDetail_View { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FactorySaleQuotationMng_FactorySaleQuotationFactorySaleQuotationAttachedFile_View> FactorySaleQuotationMng_FactorySaleQuotationFactorySaleQuotationAttachedFile_View { get; set; }
    }
}