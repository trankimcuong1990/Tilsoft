using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class FactoryOrderOverView
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

        [Display(Name = "FactoryID")]
        public int? FactoryID { get; set; }

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

        [Display(Name = "PSReceivedBy")]
        public int? PSReceivedBy { get; set; }

        [Display(Name = "IsPIReceived")]
        public bool? IsPIReceived { get; set; }

        [Display(Name = "PIReceivedDate")]
        public DateTime? PIReceivedDate { get; set; }

        [Display(Name = "PIReceivedBy")]
        public int? PIReceivedBy { get; set; }

        [Display(Name = "PaymentRemark")]
        public string PaymentRemark { get; set; }

        [Display(Name = "PSFile")]
        public string PSFile { get; set; }

        [Display(Name = "SupplyChainRemark")]
        public string SupplyChainRemark { get; set; }

        [Display(Name = "CreatedBy")]
        public int? CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "CreatorName")]
        public string CreatorName { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "TrackingStatusNM")]
        public string TrackingStatusNM { get; set; }

        [Display(Name = "TrackingStatusID")]
        public int? TrackingStatusID { get; set; }

        public string ProformaInvoiceNo { get; set; }
        public int OfferID { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public string OrderDateFormated { get; set; }
        public string LDS1Formated { get; set; }
        public string LDS2Formated { get; set; }
        public string LDS3Formated { get; set; }
        public string LDS4Formated { get; set; }
        public string PSReceivedDateFormated { get; set; }
        public string PIReceivedDateFormated { get; set; }
        public string CreatedDateFormated { get; set; }
        public string UpdatedDateFormated { get; set; }
        public List<FactoryOrderDetailOverView> FactoryOrderDetails { get; set; }
        public List<FactoryOrderSparepartDetailOverView> FactoryOrderSparepartDetails { get; set; }

        public int? V4ID { get; set; }

        public int SupplyChainPersonID { get; set; }
        public int OrderTypeID { get; set; }
        public int ItemStandardID { get; set; }
        public int TestSamplingOptionID { get; set; }
        public int LabelingOptionID { get; set; }
        public int PackagingOptionID { get; set; }
        public int InspectionOptionID { get; set; }
        public string AdditionalRemark { get; set; }
        public int ShipmentToOptionID { get; set; }
        public int ShipmentTypeOptionID { get; set; }

        public string FactoryUD { get; set; }
        public string SupplyChainPersonNM { get; set; }
        public string OrderTypeNM { get; set; }
        public string ItemStandardNM { get; set; }
        public string TestSamplingOptionNM { get; set; }
        public string LabelingOptionNM { get; set; }
        public string PackagingOptionNM { get; set; }
        public string InspectionOptionNM { get; set; }
        public string ShipmentToOptionNM { get; set; }
        public string ShipmentTypeOptionNM { get; set; }
        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
        public string ClientUD { get; set; }
        public int? ShippedByFactoryID { get; set; }
        public string ShippedByFactoryUD { get; set; }
    }
}
