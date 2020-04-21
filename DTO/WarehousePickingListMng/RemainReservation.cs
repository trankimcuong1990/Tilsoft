using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehousePickingListMng
{
    public class RemainReservation
    {
        public string RowIndex { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "ProductID")]
        public int? ProductID { get; set; }

        [Display(Name = "ProductStatusID")]
        public int? ProductStatusID { get; set; }

        [Display(Name = "RemainingReserved")]
        public int? RemainingReserved { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string ProductStatusNM { get; set; }

        public string ImageFile { get; set; }

        public string Unit { get; set; }
        public string Colli { get; set; }
        public int PackagingMethodID { get; set; }
        public int? PhysicalQnt { get; set; }

    }
}