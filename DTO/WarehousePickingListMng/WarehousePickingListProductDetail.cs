using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehousePickingListMng
{
    public class WarehousePickingListProductDetail
    {
        public int WarehousePickingListProductDetailID { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "ProductID")]
        public int? ProductID { get; set; }

        [Display(Name = "ProductStatusID")]
        public int? ProductStatusID { get; set; }

        [Display(Name = "PlaningQuantity")]
        public int? PlaningQuantity { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ProformaInvoiceNo")]
        public string ProformaInvoiceNo { get; set; }

        public string ProductStatusNM { get; set; }

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

        [Display(Name = "ItemPickingStatus")]
        public string ItemPickingStatus { get; set; }

        public bool? IsChecked { get; set; }

        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }

        public List<WarehousePickingListAreaDetail> WarehousePickingListAreaDetails { get; set; }
        public List<ModelPiece> ModelPieces { get; set; }


        public int? PhysicalQnt { get; set; }
        public int? RemainingReserved { get; set; }


    }
}