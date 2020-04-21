using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseReservationMng
{
    public class WarehouseReservationProductDetail
    {
        public int WarehouseReservationProductDetailID { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Required]
        [Display(Name = "ProductID")]
        public int? ProductID { get; set; }

        [Required]
        [Display(Name = "ProductStatusID")]
        public int? ProductStatusID { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }
    }
}