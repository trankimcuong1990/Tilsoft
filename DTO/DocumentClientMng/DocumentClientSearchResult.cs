using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.DocumentClientMng
{
    public class DocumentClientSearchResult
    {
        [Display(Name = "DocumentClientID")]
        public int DocumentClientID { get; set; }

        [Display(Name = "DateEmailToClient")]
        public DateTime? DateEmailToClient { get; set; }

        [Display(Name = "DateSendToClient")]
        public DateTime? DateSendToClient { get; set; }

        [Display(Name = "DateContainerDelivery")]
        public DateTime? DateContainerDelivery { get; set; }

        [Display(Name = "TimeContainerDelivery")]
        public DateTime? TimeContainerDelivery { get; set; }

        [Display(Name = "DHLTrackingNo")]
        public string DHLTrackingNo { get; set; }

        [Display(Name = "RemarkToClient")]
        public string RemarkToClient { get; set; }

        [Display(Name = "TypeOfDeliveryNM")]
        public string TypeOfDeliveryNM { get; set; }

        [Display(Name = "PlaceOfBargeNM")]
        public string PlaceOfBargeNM { get; set; }

        [Display(Name = "PlaceOfDeliveryNM")]
        public string PlaceOfDeliveryNM { get; set; }

        [Display(Name = "DeliveryStatusNM")]
        public string DeliveryStatusNM { get; set; }

        [Display(Name = "PaymentStatusNM")]
        public string PaymentStatusNM { get; set; }

        [Display(Name = "IsConfirmedDateContainerDelivery")]
        public bool IsConfirmedDateContainerDelivery { get; set; }

        [Display(Name = "DateContainerDeliveryConfirmerName")]
        public string DateContainerDeliveryConfirmerName { get; set; }

        [Display(Name = "ConfirmedDateContainerDeliveryDate")]
        public DateTime? ConfirmedDateContainerDeliveryDate { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "ContainerNo")]
        public string ContainerNo { get; set; }

        [Display(Name = "BLNo")]
        public string BLNo { get; set; }

        [Display(Name = "ETD")]
        public string ETD { get; set; }

        [Display(Name = "ETA")]
        public string ETA { get; set; }

        [Display(Name = "ETA2")]
        public string ETA2 { get; set; }

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

        public int IsEdit { get; set; }

        public string Season { get; set; }

        ///
        public string DateEmailToClientFormated
        {
            get
            {
                if (DateEmailToClient.HasValue)
                    return DateEmailToClient.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string DateSendToClientFormated
        {
            get
            {
                if (DateSendToClient.HasValue)
                    return DateSendToClient.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string DateContainerDeliveryFormated
        {
            get
            {
                if (DateContainerDelivery.HasValue)
                    return DateContainerDelivery.Value.ToString("dd/MM/yyyy");
                else
                    return "";
            }
        }

        public string TimeContainerDeliveryFormated
        {
            get
            {
                if (TimeContainerDelivery.HasValue)
                    return TimeContainerDelivery.Value.ToString("HH:mm");
                else
                    return "";
            }
        }



    }
}