using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class LoadingPlanDetail2
    {
        public int? LoadingPlanDetailID { get; set; }

        public int? LoadingPlanID { get; set; }

        public int? SaleOrderID { get; set; }

        public string BLNo { get; set; }

        public string ContainerNo { get; set; }

        public int? ProductID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? ProductStatusID { get; set; }

        public int? SaleOrderQnt { get; set; }

        public int? ShippedQnt { get; set; }

        public int? TotalReturnQnt { get; set; }

        public int? RemaingReturnQnt { get; set; }
        public int? ReturnQnt { get; set; }
    }
}
