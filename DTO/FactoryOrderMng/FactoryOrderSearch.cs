using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.FactoryOrderMng
{
    public class FactoryOrderSearch
    {
        [Display(Name = "FactoryOrderID")]
        public int? FactoryOrderID { get; set; }

        [Display(Name = "FactoryOrderUD")]
        public string FactoryOrderUD { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "OrderDate")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "FactoryOrderVersion")]
        public int? FactoryOrderVersion { get; set; }

        [Display(Name = "FactoryUD")]
        public string FactoryUD { get; set; }

        [Display(Name = "ProductionStatus")]
        public string ProductionStatus { get; set; }

        [Display(Name = "LDS1")]
        public DateTime? LDS1 { get; set; }

        [Display(Name = "LDS2")]
        public DateTime? LDS2 { get; set; }

        [Display(Name = "LDS3")]
        public DateTime? LDS3 { get; set; }

        [Display(Name = "LDS4")]
        public DateTime? LDS4 { get; set; }

        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [Display(Name = "IsPSReceived")]
        public bool? IsPSReceived { get; set; }

        [Display(Name = "PSReceivedDate")]
        public DateTime? PSReceivedDate { get; set; }

        [Display(Name = "PSReceiverName")]
        public string PSReceiverName { get; set; }

        [Display(Name = "IsPIReceived")]
        public bool? IsPIReceived { get; set; }

        [Display(Name = "PIReceivedDate")]
        public DateTime? PIReceivedDate { get; set; }

        [Display(Name = "PIReceiverName")]
        public string PIReceiverName { get; set; }

        [Display(Name = "PaymentRemark")]
        public string PaymentRemark { get; set; }

        [Display(Name = "PSFile")]
        public string PSFile { get; set; }

        [Display(Name = "SupplyChainRemark")]
        public string SupplyChainRemark { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "TrackingStatusNM")]
        public string TrackingStatusNM { get; set; }
        public string ProformaInvoiceNo { get; set; }

        public string OrderDateFormated
        {
            get { return (OrderDate.HasValue ? OrderDate.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string LDS1Formated
        {
            get { return (LDS1.HasValue ? LDS1.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string LDS2Formated
        {
            get { return (LDS2.HasValue ? LDS2.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string LDS3Formated
        {
            get { return (LDS3.HasValue ? LDS3.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string LDS4Formated
        {
            get { return (LDS4.HasValue ? LDS4.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string PSReceivedDateFormated
        {
            get { return (PSReceivedDate.HasValue ? PSReceivedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string PIReceivedDateFormated
        {
            get { return (PIReceivedDate.HasValue ? PIReceivedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string CreatedDateFormated
        {
            get { return (CreatedDate.HasValue ? CreatedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public string UpdatedDateFormated
        {
            get { return (UpdatedDate.HasValue ? UpdatedDate.Value.ToString("dd/MM/yyyy") : ""); }
        }

        public List<FactoryOrderDetailSearch> FactoryOrderDetailSearchs { get; set; }

    }
}