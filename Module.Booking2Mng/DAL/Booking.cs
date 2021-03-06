//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.Booking2Mng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.BookingPlanFactoryOrderDetail = new HashSet<BookingPlanFactoryOrderDetail>();
            this.BookingPlanFactoryOrderSampleDetail = new HashSet<BookingPlanFactoryOrderSampleDetail>();
            this.BookingPlanFactoryOrderSparepartDetail = new HashSet<BookingPlanFactoryOrderSparepartDetail>();
        }
    
        public int BookingID { get; set; }
        public string BookingUD { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public string ConfirmationNo { get; set; }
        public string BLNo { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> ForwarderID { get; set; }
        public string ForwarderInfo { get; set; }
        public string ShipperInfo { get; set; }
        public string ConsigneeInfo { get; set; }
        public string NotifyParty1Info { get; set; }
        public string NotifyParty2Info { get; set; }
        public string Feeder { get; set; }
        public string Vessel { get; set; }
        public Nullable<System.DateTime> ETD { get; set; }
        public Nullable<System.DateTime> ETA { get; set; }
        public Nullable<int> POLID { get; set; }
        public string PlaceOfReceipt { get; set; }
        public Nullable<int> PODID { get; set; }
        public string PlaceOfDelivery { get; set; }
        public Nullable<int> DeliveryTermID { get; set; }
        public Nullable<int> MovementTermID { get; set; }
        public string OcceanFreight { get; set; }
        public Nullable<decimal> PKGs { get; set; }
        public Nullable<decimal> KGs { get; set; }
        public Nullable<decimal> CBMs { get; set; }
        public string ShippingMark { get; set; }
        public string DescriptionOfGoods { get; set; }
        public string OriginalBL { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> ShipedDate { get; set; }
        public string Season { get; set; }
        public string DeleteRemark { get; set; }
        public Nullable<System.DateTime> CutOffDate { get; set; }
        public Nullable<System.DateTime> ETA2 { get; set; }
        public Nullable<int> ETAConfirmedBy { get; set; }
        public Nullable<int> ETA2ConfirmedBy { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<bool> IsETAConfirmed { get; set; }
        public Nullable<System.DateTime> ETAConfirmedDate { get; set; }
        public Nullable<bool> IsETA2Confirmed { get; set; }
        public Nullable<System.DateTime> ETA2ConfirmedDate { get; set; }
        public string BLFile { get; set; }
        public byte[] ConcurrencyFlag { get; set; }
        public Nullable<int> V4ID { get; set; }
        public string EUTRFile { get; set; }
        public Nullable<bool> IsConfirmAllLoading { get; set; }
        public Nullable<int> ConfirmAllLoadingBy { get; set; }
        public Nullable<System.DateTime> ConfirmAllLoadingDate { get; set; }
        public Nullable<bool> IsReset { get; set; }
        public Nullable<int> ResetBy { get; set; }
        public Nullable<System.DateTime> ResetDate { get; set; }
        public string BookingConfirmationFile { get; set; }
        public string COFile { get; set; }
        public string FumigationFile { get; set; }
        public string OtherFile { get; set; }
        public string ConfirmationFile { get; set; }
        public Nullable<bool> IsBLFileNA { get; set; }
        public Nullable<bool> IsEUTRFileNA { get; set; }
        public Nullable<bool> IsCOFileNA { get; set; }
        public Nullable<bool> IsFumigationFileNA { get; set; }
        public Nullable<bool> IsOtherFileNA { get; set; }
        public Nullable<bool> IsConfirmationFileNA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingPlanFactoryOrderDetail> BookingPlanFactoryOrderDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingPlanFactoryOrderSampleDetail> BookingPlanFactoryOrderSampleDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingPlanFactoryOrderSparepartDetail> BookingPlanFactoryOrderSparepartDetail { get; set; }
    }
}
