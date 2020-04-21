using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BookingMng
{
    public class Booking
    {
        public int BookingID { get; set; }

        [Display(Name = "BookingUD")]
        public string BookingUD { get; set; }

        [Display(Name = "BookingDate")]
        public string BookingDate { get; set; }

        [Display(Name = "ConfirmationNo")]
        public string ConfirmationNo { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "SupplierID")]
        public int? SupplierID { get; set; }

        [Display(Name = "ForwarderID")]
        public int? ForwarderID { get; set; }

        [Display(Name = "ForwarderInfo")]
        public string ForwarderInfo { get; set; }

        [Display(Name = "ShipperInfo")]
        public string ShipperInfo { get; set; }

        [Display(Name = "ConsigneeInfo")]
        public string ConsigneeInfo { get; set; }

        [Display(Name = "NotifyParty1Info")]
        public string NotifyParty1Info { get; set; }

        [Display(Name = "NotifyParty2Info")]
        public string NotifyParty2Info { get; set; }

        [Display(Name = "Feeder")]
        public string Feeder { get; set; }

        [Display(Name = "Vessel")]
        public string Vessel { get; set; }

        [Display(Name = "ETD")]
        public string ETD { get; set; }

        [Display(Name = "ETA")]
        public string ETA { get; set; }

        [Display(Name = "POLID")]
        public int? POLID { get; set; }

        [Display(Name = "PlaceOfReceipt")]
        public string PlaceOfReceipt { get; set; }

        [Display(Name = "PODID")]
        public int? PODID { get; set; }

        [Display(Name = "PlaceOfDelivery")]
        public string PlaceOfDelivery { get; set; }

        [Display(Name = "DeliveryTermID")]
        public int? DeliveryTermID { get; set; }

        [Display(Name = "MovementTermID")]
        public int? MovementTermID { get; set; }

        [Display(Name = "OcceanFreight")]
        public string OcceanFreight { get; set; }

        [Display(Name = "PKGs")]
        public decimal? PKGs { get; set; }

        [Display(Name = "KGs")]
        public decimal? KGs { get; set; }

        [Display(Name = "CBMs")]
        public decimal? CBMs { get; set; }

        [Display(Name = "ShippingMark")]
        public string ShippingMark { get; set; }

        [Display(Name = "DescriptionOfGoods")]
        public string DescriptionOfGoods { get; set; }

        [Display(Name = "OriginalBL")]
        public string OriginalBL { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "ConfirmedBy")]
        public int? ConfirmedBy { get; set; }

        [Display(Name = "ConfirmedDate")]
        public string ConfirmedDate { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public string CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "ShipedDate")]
        public string ShipedDate { get; set; }

        [Display(Name = "Season")]
        public string Season { get; set; }

        [Display(Name = "CutOffDate")]
        public string CutOffDate { get; set; }

        [Display(Name = "ETA2")]
        public string ETA2 { get; set; }

        [Display(Name = "ETAConfirmedBy")]
        public int? ETAConfirmedBy { get; set; }

        [Display(Name = "ETA2ConfirmedBy")]
        public int? ETA2ConfirmedBy { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "IsETAConfirmed")]
        public bool? IsETAConfirmed { get; set; }

        [Display(Name = "ETAConfirmedDate")]
        public string ETAConfirmedDate { get; set; }

        [Display(Name = "IsETA2Confirmed")]
        public bool? IsETA2Confirmed { get; set; }

        [Display(Name = "ETA2ConfirmedDate")]
        public string ETA2ConfirmedDate { get; set; }

        [Display(Name = "BLFile")]
        public string BLFile { get; set; }

        [Display(Name = "SupplierNM")]
        public string SupplierNM { get; set; }

        [Display(Name = "ForwarderNM")]
        public string ForwarderNM { get; set; }

        [Display(Name = "PoDName")]
        public string PoDName { get; set; }

        [Display(Name = "PoLname")]
        public string PoLname { get; set; }

        public string ClientUD { get; set; }

        [Display(Name = "DeliveryTermNM")]
        public string DeliveryTermNM { get; set; }

        [Display(Name = "MovementTermNM")]
        public string MovementTermNM { get; set; }

        public byte[] ConcurrencyFlag { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public List<LoadingPlan>  Details { get; set; }

        public string BLFileURL { get; set; }
        public string BLFileFriendlyName { get; set; }
        public bool BLFileHasChange {get;set;}
        public string NewBLFile { get; set; }

        public string ConfirmerName { get; set; }
        public string UpdatorName { get; set; }
        public string ETAConfirmerName { get; set; }
        public string ETA2ConfirmerName { get; set; }
    }
}
