using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseImportMng
{
    public class WarehouseImportSearchResult
    {
        public int WarehouseImportID { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "ReceiptNo")]
        public string ReceiptNo { get; set; }

        [Display(Name = "ImportedDate")]
        public string ImportedDate { get; set; }

        [Display(Name = "UpdatorName")]
        public string UpdatorName { get; set; }

        [Display(Name = "UpdatedDate")]
        public string UpdatedDate { get; set; }

        [Display(Name = "ConfirmerName")]
        public string ConfirmerName { get; set; }

        [Display(Name = "ConfirmedDate")]
        public string ConfirmedDate { get; set; }

        public string ImportTypeNM { get; set; }
        public int ImportTypeID { get; set; }
        public string RefNo { get; set; }
        public string Season { get; set; }

        public string Remark { get; set; }

        public int UpdatedBy { get; set; }
        public int ConfirmedBy { get; set; }
    }
}