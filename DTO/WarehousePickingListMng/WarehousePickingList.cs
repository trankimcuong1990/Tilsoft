using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehousePickingListMng
{
    public class WarehousePickingList
    {
        public int WarehousePickingListID { get; set; }

        [Display(Name = "ReceiptNo")]
        public string ReceiptNo { get; set; }

        [Display(Name = "PlaningDate")]
        public string PlaningDate { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "ProcessingStatusID")]
        public int? ProcessingStatusID { get; set; }

        [Display(Name = "StatusUpdatedBy")]
        public int? StatusUpdatedBy { get; set; }

        [Display(Name = "StatusUpdatedDate")]
        public string StatusUpdatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "StatusUpdatorName")]
        public string StatusUpdatorName { get; set; }

        [Display(Name = "ProcessingStatusNM")]
        public string ProcessingStatusNM { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        public string ConcurrencyFlag_String { get; set; }
        public List<WarehousePickingListProductDetail> Details { get; set; }
        public List<WarehousePickingListSparepartDetail> SparepartDetails { get; set; }


        [Display(Name = "PlaningTime")]
        public string PlaningTime { get; set; }

        [Display(Name = "EuroPalletQnt")]
        public int? EuroPalletQnt { get; set; }

        [Display(Name = "IsInvoicePalletToClient")]
        public bool? IsInvoicePalletToClient { get; set; }

        [Display(Name = "RealPickingDate")]
        public string RealPickingDate { get; set; }

        [Display(Name = "PickingBy")]
        public int? PickingBy { get; set; }

        [Display(Name = "PlannedBy")]
        public int? PlannedBy { get; set; }

        [Display(Name = "PickingStatus")]
        public string PickingStatus { get; set; }

        [Display(Name = "CMRNo")]
        public string CMRNo { get; set; }

        public string CMR1 { get; set; }
        public string CMR2 { get; set; }
        public string CMR3 { get; set; }
        public string CMR4 { get; set; }
        public string CMR5 { get; set; }
        public string CMR13 { get; set; }
        public string CMR14 { get; set; }
        public string CMR15 { get; set; }
        public string CMR16 { get; set; }
        public string CMR17 { get; set; }
        public string CMR18 { get; set; }
        public string CMR19 { get; set; }
        public string CMR20 { get; set; }
        public string CMR21 { get; set; }
        public string CMR22 { get; set; }
        public string CMR23 { get; set; }
        public string CMR24 { get; set; }
        public string CMRDate { get; set; }
        public string InvoicedStatus { get; set; }
        public string Season { get; set; }
        public string RefNo { get; set; }
        public string DeliveryDate { get; set; }

        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderZipCode { get; set; }
        public string SenderCity { get; set; }
        public string SenderCountry { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverZipCode { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverCountry { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
    }
}