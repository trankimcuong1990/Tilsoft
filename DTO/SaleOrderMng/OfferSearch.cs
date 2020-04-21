using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class OfferSearch
    {
        [Display(Name = "OfferID")]
        public int? OfferID { get; set; }

        [Display(Name = "OfferUD")]
        public string OfferUD { get; set; }

        [Display(Name = "OfferDate")]
        public DateTime? OfferDate { get; set; }

        [Display(Name = "OfferVersion")]
        public int? OfferVersion { get; set; }

        [Display(Name = "TrackingStatusNM")]
        public string TrackingStatusNM { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "SaleID")]
        public int SaleID { get; set; }

        [Display(Name = "SaleNM")]
        public string SaleNM { get; set; }

        [Display(Name = "ValidUntil")]
        public DateTime? ValidUntil { get; set; }

        [Display(Name = "LDS")]
        public DateTime? LDS { get; set; }

        [Display(Name = "PODName")]
        public string PODName { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "ForwarderNM")]
        public string ForwarderNM { get; set; }

        [Display(Name = "ForwarderContractNumber")]
        public string ForwarderContractNumber { get; set; }

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
        public string OfferDateFormated
        {
            get { return (OfferDate.HasValue ? OfferDate.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string ValidUntilFormated
        {
            get { return (ValidUntil.HasValue ? ValidUntil.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public string LDSFormated
        {
            get { return (LDS.HasValue ? LDS.Value.ToString("dd/MM/yyyy") : ""); }
        }
        public List<SaleOrderSearch> SaleOrderSearchs { get; set; }

        public int? NumOfPIs { get; set; }

        public int UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string FullName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDateFormatted
        {
            get
            {
                return (UpdatedDate.HasValue ? UpdatedDate.Value.ToString("dd/MM/yyyy") : string.Empty);
            }
        }
    }
}