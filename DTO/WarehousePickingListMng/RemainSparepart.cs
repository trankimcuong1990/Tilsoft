using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehousePickingListMng
{
    public class RemainSparepart
    {
        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "RemainingReserved")]
        public int? RemainingReserved { get; set; }

        [Display(Name = "ClientID")]
        public int? ClientID { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "SaleOrderDetailSparepartID")]
        public int? SaleOrderDetailSparepartID { get; set; }

        [Display(Name = "SparepartID")]
        public int? SparepartID { get; set; }

        [Display(Name = "ClientUD")]
        public string ClientUD { get; set; }

        [Display(Name = "ClientNM")]
        public string ClientNM { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Colli")]
        public string Colli { get; set; }

        [Display(Name = "PackagingMethodID")]
        public int? PackagingMethodID { get; set; }

        [Display(Name = "ClientGroupID")]
        public int? ClientGroupID { get; set; }

        public bool IsSelected { get; set; }

        public int? ProductStatusID { get; set; }
        public string ProductStatusNM { get; set; }

        public int? PhysicalQnt { get; set; }

    }
}