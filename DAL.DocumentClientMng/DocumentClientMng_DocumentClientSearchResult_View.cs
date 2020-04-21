//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DocumentClientMng
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocumentClientMng_DocumentClientSearchResult_View
    {
        public int DocumentClientID { get; set; }
        public Nullable<System.DateTime> DateEmailToClient { get; set; }
        public Nullable<System.DateTime> DateSendToClient { get; set; }
        public Nullable<System.DateTime> DateContainerDelivery { get; set; }
        public Nullable<System.DateTime> TimeContainerDelivery { get; set; }
        public string DHLTrackingNo { get; set; }
        public string RemarkToClient { get; set; }
        public string TypeOfDeliveryNM { get; set; }
        public string PlaceOfBargeNM { get; set; }
        public string PlaceOfDeliveryNM { get; set; }
        public string DeliveryStatusNM { get; set; }
        public string PaymentStatusNM { get; set; }
        public Nullable<bool> IsConfirmedDateContainerDelivery { get; set; }
        public string DateContainerDeliveryConfirmerName { get; set; }
        public Nullable<System.DateTime> ConfirmedDateContainerDeliveryDate { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ContainerNo { get; set; }
        public string BLNo { get; set; }
        public Nullable<System.DateTime> ETD { get; set; }
        public Nullable<System.DateTime> ETA { get; set; }
        public Nullable<System.DateTime> ETA2 { get; set; }
        public string ForwarderNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<int> DeliveryStatusID { get; set; }
        public string Season { get; set; }
    }
}