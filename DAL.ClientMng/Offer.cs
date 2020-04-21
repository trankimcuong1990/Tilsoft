//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.ClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class Offer
    {
        public int OfferID { get; set; }
        public string OfferUD { get; set; }
        public Nullable<System.DateTime> OfferDate { get; set; }
        public Nullable<int> OfferVersion { get; set; }
        public Nullable<int> OfferStatusID { get; set; }
        public string Season { get; set; }
        public Nullable<int> SaleID { get; set; }
        public string ClientContactPerson { get; set; }
        public string ClientContractPerson { get; set; }
        public Nullable<System.DateTime> ValidUntil { get; set; }
        public Nullable<int> PaymentTermID { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<System.DateTime> LDS { get; set; }
        public Nullable<System.DateTime> EstimatedDeliveryDate { get; set; }
        public Nullable<int> PODID { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<decimal> SurChargePercent { get; set; }
        public Nullable<decimal> CommissionPercent { get; set; }
        public Nullable<decimal> TransportationFee { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public string LabelingType { get; set; }
        public string LabelingNote { get; set; }
        public Nullable<bool> IsLabelingNeedToFollowUp { get; set; }
        public string PackagingType { get; set; }
        public string PackagingNote { get; set; }
        public Nullable<bool> IsPackagingNeedToFollowUp { get; set; }
        public Nullable<int> ForwarderID { get; set; }
        public string ForwarderContractNo { get; set; }
        public Nullable<int> ForwaderInfoFollowUpBy { get; set; }
        public Nullable<int> PutInProductionTermID { get; set; }
        public string PutInProductionRemark { get; set; }
        public Nullable<decimal> QntIn20DC { get; set; }
        public Nullable<decimal> QntIn40DC { get; set; }
        public Nullable<decimal> QntIn40HC { get; set; }
        public Nullable<decimal> PartialQnt { get; set; }
        public string QntRemark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> V4ID { get; set; }
        public string TypeOfOrder { get; set; }
        public string OfferScanFile { get; set; }
        public Nullable<bool> IsBackOffer { get; set; }
        public string ClientContactEmail { get; set; }
        public string ClientContactPhone { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsPotential { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
    
        public virtual Client Client { get; set; }
    }
}
