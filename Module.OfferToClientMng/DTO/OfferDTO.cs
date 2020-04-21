using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Module.OfferToClientMng.DTO
{
    public class OfferDTO
    {
        [Display(Name = "OfferID")]
        public int? OfferID { get; set; }

        [Display(Name = "OfferUD")]
        public string OfferUD { get; set; }

        [Display(Name = "OfferDate")]
        public string OfferDate { get; set; }

        [Display(Name = "OfferVersion")]
        public int? OfferVersion { get; set; }

        [Display(Name = "OfferStatusID")]
        public int? OfferStatusID { get; set; }

        [Display(Name = "Season")]
        public string Season { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "SaleID")]
        public int? SaleID { get; set; }

        [Display(Name = "ClientContactPerson")]
        public string ClientContactPerson { get; set; }

        [Display(Name = "ClientContractPerson")]
        public string ClientContractPerson { get; set; }

        [Display(Name = "ValidUntil")]
        public string ValidUntil { get; set; }

        [Display(Name = "PaymentTermID")]
        public int? PaymentTermID { get; set; }

        [Display(Name = "DeliveryTermID")]
        public int? DeliveryTermID { get; set; }

        [Display(Name = "LDS")]
        public string LDS { get; set; }

        [Display(Name = "EstimatedDeliveryDate")]
        public string EstimatedDeliveryDate { get; set; }

        [Display(Name = "PODID")]
        public int? PODID { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "ExchangeRate")]
        public decimal? ExchangeRate { get; set; }

        [Display(Name = "SurChargePercent")]
        public decimal? SurChargePercent { get; set; }

        [Display(Name = "CommissionPercent")]
        public decimal? CommissionPercent { get; set; }

        [Display(Name = "TransportationFee")]
        public decimal? TransportationFee { get; set; }

        [Display(Name = "VAT")]
        public decimal? VAT { get; set; }

        [Display(Name = "LabelingType")]
        public string LabelingType { get; set; }

        [Display(Name = "LabelingNote")]
        public string LabelingNote { get; set; }

        [Display(Name = "IsLabelingNeedToFollowUp")]
        public bool? IsLabelingNeedToFollowUp { get; set; }

        [Display(Name = "PackagingType")]
        public string PackagingType { get; set; }

        [Display(Name = "PackagingNote")]
        public string PackagingNote { get; set; }

        [Display(Name = "IsPackagingNeedToFollowUp")]
        public bool? IsPackagingNeedToFollowUp { get; set; }

        [Display(Name = "ForwarderID")]
        public int? ForwarderID { get; set; }

        [Display(Name = "ForwarderContractNo")]
        public string ForwarderContractNo { get; set; }

        [Display(Name = "ForwaderInfoFollowUpBy")]
        public int? ForwaderInfoFollowUpBy { get; set; }

        [Display(Name = "PutInProductionTermID")]
        public int? PutInProductionTermID { get; set; }

        [Display(Name = "PutInProductionRemark")]
        public string PutInProductionRemark { get; set; }

        [Display(Name = "QntIn20DC")]
        public decimal? QntIn20DC { get; set; }

        [Display(Name = "QntIn40DC")]
        public decimal? QntIn40DC { get; set; }

        [Display(Name = "QntIn40HC")]
        public decimal? QntIn40HC { get; set; }

        [Display(Name = "PartialQnt")]
        public decimal? PartialQnt { get; set; }

        [Display(Name = "QntRemark")]
        public string QntRemark { get; set; }


        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public string CreatedDate { get; set; }


        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "TrackingStatusID")]
        public int? TrackingStatusID { get; set; }

        [Display(Name = "TrackingStatusNM")]
        public string TrackingStatusNM { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "ClientTelephone")]
        public string ClientTelephone { get; set; }

        [Display(Name = "ClientEmail")]
        public string ClientEmail { get; set; }

        [Display(Name = "ClientStreetAddress1")]
        public string ClientStreetAddress1 { get; set; }

        [Display(Name = "ClientStreetAddress2")]
        public string ClientStreetAddress2 { get; set; }

        [Display(Name = "ClientZipCode")]
        public string ClientZipCode { get; set; }

        [Display(Name = "ForwarderUD")]
        public string ForwarderUD { get; set; }

        [Display(Name = "ForwarderPIC")]
        public string ForwarderPIC { get; set; }

        [Display(Name = "ForwarderMobile")]
        public string ForwarderMobile { get; set; }

        [Display(Name = "ForwarderEmail")]
        public string ForwarderEmail { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public string OfferDateFormated { get; set; }
        public string ValidUntilFormated { get; set; }
        public string LDSFormated { get; set; }
        public string EstimatedDeliveryDateFormated { get; set; }
        public List<OfferLineDTO> OfferLineDTOs { get; set; }
        public List<OfferLineSparepartDTO> OfferLineSparepartDTOs { get; set; }
        public List<OfferLineSampleProductDTO> OfferLineSampleProductDTOs { get; set; }
        public int? V4ID { get; set; }
        public string OfferScanFile { get; set; }

        public string OfferScanFileURL { get; set; }
        public string OfferScanFileFriendlyName { get; set; }
        public bool OfferScanFileHasChange { get; set; }
        public string NewOfferScanFile { get; set; }

        public string ClientContactEmail { get; set; }

        public string ClientContactPhone { get; set; }

        public string Remark { get; set; }
        public bool? IsPotential { get; set; }

        public bool? IsApproved { get; set; }
        public string ApproverName { get; set; }
        public string ApprovedDate { get; set; }

        public Nullable<decimal> InterestCostPercent { get; set; }
        public Nullable<decimal> InspectionCostUSD { get; set; }
        public Nullable<decimal> LCCostPercent { get; set; }
        public Nullable<decimal> SampleCostUSD { get; set; }
        public Nullable<decimal> DefaultCommissionPercent { get; set; }
        public Nullable<decimal> BonusPercent { get; set; }
        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public string UpdatedDateFormated { get; set; }
        public string CreatedDateFormated { get; set; }      
    }
}
