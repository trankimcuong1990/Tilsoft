using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class FactoryOrderDetailOverView
    {
        [Display(Name = "FactoryOrderDetailID")]
        public int? FactoryOrderDetailID { get; set; }

        [Display(Name = "FactoryOrderID")]
        public int? FactoryOrderID { get; set; }

        [Display(Name = "SaleOrderDetailID")]
        public int? SaleOrderDetailID { get; set; }

        [Display(Name = "OrderQnt")]
        public int? OrderQnt { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "ArticleCode")]
        public string ArticleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "SaleOrderQnt")]
        public int? SaleOrderQnt { get; set; }
        public int? V4ID { get; set; }

        [Display(Name = "TotalQnt")]
        public int? TotalQnt { get; set; }

        [Display(Name = "RemainQnt")]
        public int? RemainQnt { get; set; }

        public int? ModelID { get; set; }
        public int? ProductID { get; set; }
        public int? BOMStandardID { get; set; }
        public int? BOMID { get; set; }

        public List<FactoryOrderColliDetailOverView> FactoryOrderColliDetails { get; set; }
    }
}
