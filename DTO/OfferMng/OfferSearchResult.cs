using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.OfferMng
{
    public class OfferSearchResult
    {
        public int OfferID { get; set; }

        [Display(Name = "OfferUD")]
        public string OfferUD { get; set; }

        [Display(Name = "OfferDate")]
        public string OfferDate { get; set; }

        [Display(Name = "OfferVersion")]
        public int OfferVersion { get; set; }

        [Display(Name = "Season")]
        public string Season { get; set; }

        [Display(Name = "ValidUntil")]
        public string ValidUntil { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "PaymentTermNM")]
        public string PaymentTermNM { get; set; }

        [Display(Name = "DeliveryTermNM")]
        public string DeliveryTermNM { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "LDS")]
        public string LDS { get; set; }

        [Display(Name = "ForwarderNM")]
        public string ForwarderNM { get; set; }

        [Display(Name = "PODNM")]
        public string PODNM { get; set; }

        [Display(Name = "UpdatedBy")]
        public int UpdatedBy { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }
        public int? V4ID { get; set; }
        public string TrackingStatusNM { get; set; }
        public int? TrackingStatusID { get; set; }
        public bool? IsPotential { get; set; }
        public int? SaleID { get; set; }

        public bool? IsApproved { get; set; }
        public string ApproverName { get; set; }
        public string ApprovedDate { get; set; }
        public decimal? TotalAmount { get; set; }

        public string CreatorName { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public int TotalItemToBeApproved { get; set; }
    }
}