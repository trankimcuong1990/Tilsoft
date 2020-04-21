//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.FactoryOrderMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class FactoryOrderMng_FactoryOrder_View
    {
        public FactoryOrderMng_FactoryOrder_View()
        {
            this.FactoryOrderMng_FactoryOrderDetail_View = new HashSet<FactoryOrderMng_FactoryOrderDetail_View>();
            this.FactoryOrderMng_FactoryOrderSparepartDetail_View = new HashSet<FactoryOrderMng_FactoryOrderSparepartDetail_View>();
            this.FactoryOrderMng_FactoryOrderSampleDetail_View = new HashSet<FactoryOrderMng_FactoryOrderSampleDetail_View>();
        }
    
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public Nullable<int> SaleOrderID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> FactoryOrderVersion { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string ProductionStatus { get; set; }
        public Nullable<System.DateTime> LDS1 { get; set; }
        public Nullable<System.DateTime> LDS2 { get; set; }
        public Nullable<System.DateTime> LDS3 { get; set; }
        public Nullable<System.DateTime> LDS4 { get; set; }
        public string Rating { get; set; }
        public Nullable<bool> IsPSReceived { get; set; }
        public Nullable<System.DateTime> PSReceivedDate { get; set; }
        public Nullable<int> PSReceivedBy { get; set; }
        public Nullable<bool> IsPIReceived { get; set; }
        public Nullable<System.DateTime> PIReceivedDate { get; set; }
        public Nullable<int> PIReceivedBy { get; set; }
        public string PaymentRemark { get; set; }
        public string PSFile { get; set; }
        public string SupplyChainRemark { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string TrackingStatusNM { get; set; }
        public Nullable<int> TrackingStatusID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<int> OfferID { get; set; }
        public Nullable<int> V4ID { get; set; }
        public Nullable<int> SupplyChainPersonID { get; set; }
        public Nullable<int> OrderTypeID { get; set; }
        public Nullable<int> ItemStandardID { get; set; }
        public Nullable<int> TestSamplingOptionID { get; set; }
        public Nullable<int> LabelingOptionID { get; set; }
        public Nullable<int> PackagingOptionID { get; set; }
        public Nullable<int> InspectionOptionID { get; set; }
        public string AdditionalRemark { get; set; }
        public Nullable<int> ShipmentToOptionID { get; set; }
        public Nullable<int> ShipmentTypeOptionID { get; set; }
        public string FactoryUD { get; set; }
        public string SupplyChainPersonNM { get; set; }
        public string OrderTypeNM { get; set; }
        public string ItemStandardNM { get; set; }
        public string TestSamplingOptionNM { get; set; }
        public string LabelingOptionNM { get; set; }
        public string PackagingOptionNM { get; set; }
        public string InspectionOptionNM { get; set; }
        public string ShipmentToOptionNM { get; set; }
        public string ShipmentTypeOptionNM { get; set; }
        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> ShippedByFactoryID { get; set; }
        public string ShippedByFactoryUD { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        public virtual ICollection<FactoryOrderMng_FactoryOrderDetail_View> FactoryOrderMng_FactoryOrderDetail_View { get; set; }
        public virtual ICollection<FactoryOrderMng_FactoryOrderSparepartDetail_View> FactoryOrderMng_FactoryOrderSparepartDetail_View { get; set; }
        public virtual ICollection<FactoryOrderMng_FactoryOrderSampleDetail_View> FactoryOrderMng_FactoryOrderSampleDetail_View { get; set; }
    }
}
