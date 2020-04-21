using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.WarehouseReservationMng
{
    public class WarehouseReservation
    {
        public int WarehouseReservationID { get; set; }

        [Display(Name = "ReceiptNo")]
        public string ReceiptNo { get; set; }

        [Display(Name = "ReservedDate")]
        public DateTime? ReservedDate { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "UpdatedBy")]
        public int? UpdatedBy { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "ProcessingStatusID")]
        public int? ProcessingStatusID { get; set; }

        [Display(Name = "StatusUpdatedBy")]
        public int? StatusUpdatedBy { get; set; }

        [Display(Name = "StatusUpdatedDate")]
        public DateTime? StatusUpdatedDate { get; set; }

        [Display(Name = "ConcurrencyFlag")]
        public object ConcurrencyFlag { get; set; }

        public List<WarehouseReservationProductDetail> Details { get; set; }

    }
}