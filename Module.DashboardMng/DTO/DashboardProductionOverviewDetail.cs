using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardProductionOverviewDetail
    {
        public int? FactoryOrderDetailID { get; set; }
        public int? ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string ArtDescription { get; set; }
        public string QuotationStatusNM { get; set; }
        public object SalePrice { get; set; }
        public decimal? OrderQnt { get; set; }
        public decimal? NumContOrderd { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? ShippedQnt { get; set; }
        public decimal? NumContShipped { get; set; }
        public decimal? NumContTobeShipped { get; set; }
    }
}
