using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class WarehouseImportType
    {
        public int ConstantEntryID { get; set; }

        [Display(Name = "ImportTypeID")]
        public int? ImportTypeID { get; set; }

        [Display(Name = "ImportTypeNM")]
        public string ImportTypeNM { get; set; }

        [Display(Name = "DisplayOrder")]
        public int? DisplayOrder { get; set; }

    }

    public class FreeToSaleCategory {
        public int FreeToSaleCategoryID { get; set; }
        public string FreeToSaleCategoryNM { get; set; }
    }
}