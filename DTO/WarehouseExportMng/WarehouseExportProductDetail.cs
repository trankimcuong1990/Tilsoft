using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseExportMng
{
    public class WarehouseExportProductDetail
    {
        public int WarehouseExportProductDetailID { get; set; }

        [Display(Name = "ProductID")]
        public int? ProductID { get; set; }

        [Display(Name = "ProductStatusID")]
        public int? ProductStatusID { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "ProductStatusNM")]
        public string ProductStatusNM { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

    }
}