using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseImportMng
{
    public class WarehouseImport
    {
        public int WarehouseImportID { get; set; }

        [Display(Name = "ReceiptNo")]
        public string ReceiptNo { get; set; }

        [Display(Name = "ImportedDate")]
        public string ImportedDate { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "ConfirmedBy")]
        public int? ConfirmedBy { get; set; }

        [Display(Name = "ConfirmedDate")]
        public string ConfirmedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public byte[] ConcurrencyFlag { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        public string ConcurrencyFlag_String { get; set; }

        public int ImportTypeID { get; set; }

        public string ImportType { get; set; }

        public string RefNo { get; set; }

        public string Season { get; set; }

        public int? ECommercialInvoiceID { get; set; }

        public int? SaleOrderID { get; set; }

        public List<WarehouseImportProductDetail> Details { get; set; }

        public List<WarehouseImportSparepartDetail> SparepartDetails { get; set; }

        public string ContainerReceivedDate { get; set; }
        public string ContainerReceivedTime { get; set; }
        public bool? IsWexImported { get; set; }
    }
}