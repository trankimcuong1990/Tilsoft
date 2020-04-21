using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehousePickingListMng
{
    public class WarehousePickingListSparepartDetail
    {
        public int WarehousePickingListSparepartDetailID { get; set; }

        [Display(Name = "SaleOrderDetailSparepartID")]
        public int? SaleOrderDetailSparepartID { get; set; }

        [Display(Name = "SparepartID")]
        public int? SparepartID { get; set; }

        [Display(Name = "PlaningQuantity")]
        public int? PlaningQuantity { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "WarehouseAreaID")]
        public int? WarehouseAreaID { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Colli")]
        public string Colli { get; set; }

        [Display(Name = "PackagingMethodID")]
        public int? PackagingMethodID { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        public string ItemPickingStatus { get; set; }

        public string ArticleCode { get; set; }
        public string Description { get; set; }

        public int? ProductStatusID { get; set; }
        public string ProductStatusNM { get; set; }
        public string ProformaInvoiceNo { get; set; }

        public bool? IsChecked { get; set; }

        public List<WarehousePickingListAreaDetail> WarehousePickingListAreaDetails { get; set; }

        public int? PhysicalQnt { get; set; }
        public int? RemainingReserved { get; set; }

    }
}