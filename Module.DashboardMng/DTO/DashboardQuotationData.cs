using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DashboardMng.DTO
{
    public class DashboardQuotationData
    {
        public int Id { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public int OrderQnt { get; set; }
        public decimal? AVTPrice { get; set; }
        public decimal? FactoryPrice { get; set; }
        public string LastUpdatedDate { get; set; }
        public int? PriceUpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string StatusName { get; set; }
        public bool? HaveUrlLink { get; set; }
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public int QuotationDetailID { get; set; }
        public int? QuotationID { get; set; }
        public int? StatusID { get; set; }
        public int IsSetPrice { get; set; }
    }
}
