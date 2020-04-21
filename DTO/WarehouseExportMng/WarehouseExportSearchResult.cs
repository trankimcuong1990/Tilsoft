using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseExportMng
{
    public class WarehouseExportSearchResult
    {
        public int WarehouseExportID { get; set; }

        [Display(Name = "ReceiptNo")]
        public string ReceiptNo { get; set; }

        [Display(Name = "CMRNo")]
        public string CMRNo { get; set; }

        [Display(Name = "WarehousePickingListNo")]
        public string WarehousePickingListNo { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "ExportedDate")]
        public string ExportedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "ProcessingStatusNM")]
        public string ProcessingStatusNM { get; set; }

        [Display(Name = "StatusUpdatorName")]
        public string StatusUpdatorName { get; set; }

        [Display(Name = "StatusUpdatedDate")]
        public string StatusUpdatedDate { get; set; }

    }
}