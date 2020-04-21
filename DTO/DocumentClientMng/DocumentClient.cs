using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DocumentClientMng
{
    public class DocumentClient
    {

        [Display(Name = "DocumentClientID")]
        public int DocumentClientID { get; set; }
        [Required]
        [Display(Name = "LoadingPlanID")]
        public int LoadingPlanID { get; set; }

        [Display(Name = "Date Email To Client")]
        public DateTime? DateEmailToClient { get; set; }

        [Display(Name = "Date Send To Client")]
        public DateTime? DateSendToClient { get; set; }

        [Display(Name = "Date Container Delivery")]
        public DateTime? DateContainerDelivery { get; set; }

        [Display(Name = "Time Container Delivery")]
        public DateTime? TimeContainerDelivery { get; set; }

        [Display(Name = "DHLTrackingNo")]
        public string DHLTrackingNo { get; set; }

        [Display(Name = "Remark To Client")]
        public object RemarkToClient { get; set; }

        [Display(Name = "Type Of DeliveryID")]
        public int? TypeOfDeliveryID { get; set; }

        [Display(Name = "Place Of Barge")]
        public int? PlaceOfBargeID { get; set; }

        [Display(Name = "PlaceOfDeliveryID")]
        public int? PlaceOfDeliveryID { get; set; }

        [Display(Name = "DeliveryStatusID")]
        public int? DeliveryStatusID { get; set; }

        [Display(Name = "PaymentStatusID")]
        public int? PaymentStatusID { get; set; }

        [Display(Name = "IsConfirmedDateContainerDelivery")]
        public bool IsConfirmedDateContainerDelivery { get; set; }

        [Display(Name = "ConfirmedDateContainerDeliveryBy")]
        public int? ConfirmedDateContainerDeliveryBy { get; set; }

        [Display(Name = "ConfirmedDateContainerDeliveryDate")]
        public DateTime? ConfirmedDateContainerDeliveryDate { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "ContainerNo")]
        public string ContainerNo { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "ETA")]
        public DateTime ETA { get; set; }

        [Display(Name = "ETA2")]
        public DateTime? ETA2 { get; set; }

        [Display(Name = "ForwarderNM")]
        public string ForwarderNM { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        [Display(Name = "InvoiceNo")]
        public string InvoiceNo { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "DateContainerDeliveryConfirmerName")]
        public string DateContainerDeliveryConfirmerName { get; set; }
        public string DateEmailToClientFormated { get; set; }
        public string DateSendToClientFormated { get; set; }
        public string DateContainerDeliveryFormated { get; set; }
        public string TimeContainerDeliveryFormated { get; set; }
        public string CreatedDateFormated { get; set; }
        public string UpdatedDateFormated { get; set; }
        public string ETDFormated { get; set; }
        public string ETAFormated { get; set; }
        public string ETA2Formated { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string BLFileURL { get; set; }
        public string BLFileFriendlyName { get; set; }
        public List<ECommercialInvoice> ECommercialInvoices { get; set; }
        public string ConfirmedDateContainerDeliveryDateFormated { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
    }
}