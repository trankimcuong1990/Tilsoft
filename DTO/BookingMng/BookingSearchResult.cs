using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BookingMng
{
    public class BookingSearchResult
    {
        public int BookingID { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "Season")]
        public string Season { get; set; }

        [Display(Name = "BookingUD")]
        public string BookingUD { get; set; }

        [Display(Name = "BookingDate")]
        public string BookingDate { get; set; }

        [Display(Name = "ConfirmationNo")]
        public string ConfirmationNo { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "SupplierNM")]
        public string SupplierNM { get; set; }

        [Display(Name = "ForwarderNM")]
        public string ForwarderNM { get; set; }

        [Display(Name = "PoDName")]
        public string PoDName { get; set; }

        [Display(Name = "PoLname")]
        public string PoLname { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "DeliveryTermNM")]
        public string DeliveryTermNM { get; set; }

        [Display(Name = "MovementTermNM")]
        public string MovementTermNM { get; set; }

        [Display(Name = "PlaceOfReceipt")]
        public string PlaceOfReceipt { get; set; }

        [Display(Name = "PlaceOfDelivery")]
        public string PlaceOfDelivery { get; set; }

        [Display(Name = "ShipedDate")]
        public string ShipedDate { get; set; }

        [Display(Name = "ETA")]
        public string ETA { get; set; }

        [Display(Name = "ETA2")]
        public string ETA2 { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        [Display(Name = "ConfirmedDate")]
        public string ConfirmedDate { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "CreatedDate")]
        public string CreatedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        public string FactoryUD { get; set; }
    }
}
