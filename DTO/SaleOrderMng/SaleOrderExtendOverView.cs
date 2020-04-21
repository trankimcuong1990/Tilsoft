using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.SaleOrderMng
{
    public class SaleOrderExtendOverView
    {
        [Display(Name = "SaleOrderExtendID")]
        public int? SaleOrderExtendID { get; set; }

        [Display(Name = "SaleOrderID")]
        public int? SaleOrderID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "RowIndex")]
        public int? RowIndex { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        public int? V4ID { get; set; }

        public string CostType { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
