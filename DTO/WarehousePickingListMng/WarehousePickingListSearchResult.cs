using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehousePickingListMng
{
    public class WarehousePickingListSearchResult
    {
        public int WarehousePickingListID { get; set; }

        [Display(Name = "ProcessingStatusNM")]
        public string ProcessingStatusNM { get; set; }

        [Display(Name = "ReceiptNo")]
        public string ReceiptNo { get; set; }

        [Display(Name = "PlaningDate")]
        public string PlaningDate { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "StatusUpdatorName")]
        public string StatusUpdatorName { get; set; }

        [Display(Name = "StatusUpdatedDate")]
        public string StatusUpdatedDate { get; set; }

        [Display(Name = "PlaningTime")]
        public string PlaningTime { get; set; }

        [Display(Name = "EuroPalletQnt")]
        public int EuroPalletQnt { get; set; }

        [Display(Name = "IsInvoicePalletToClient")]
        public string IsInvoicePalletToClient { get; set; }

        [Display(Name = "RealPickingDate")]
        public string RealPickingDate { get; set; }

        [Display(Name = "PickerName")]
        public string PickerName { get; set; }

        [Display(Name = "PlannerName")]
        public string PlannerName { get; set; }

        [Display(Name = "PickingStatus")]
        public string PickingStatus { get; set; }

        public string InvoicedStatus { get; set; }
        public string CMRNo { get; set; }
        public string CMR2 { get; set; }

        public int UpdatedBy { get; set; }
        public int StatusUpdatedBy { get; set; }
    }
}