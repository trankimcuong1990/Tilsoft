using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseCIMng
{
    public class WarehouseCIDetail
    {
        [Display(Name = "WarehouseCIDetailID")]
        public int? WarehouseCIDetailID { get; set; }

        [Display(Name = "WarehouseCIID")]
        public int? WarehouseCIID { get; set; }

        [Display(Name = "ProductID")]
        public int? ProductID { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

    }
}