using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class SaleOrderDetailExtend
    {
        [Display(Name = "SaleOrderDetailExtendID")]
        public int? SaleOrderDetailExtendID { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "RowIndex")]
        public int? RowIndex { get; set; }

        public int? V4ID { get; set; }

    }
}