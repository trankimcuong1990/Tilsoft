using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models.ReportDTO
{
    public class LoadingPlanDetailReportDTO
    {
        public int? LoadingPlanDetailID { get; set; }
        public int? LoadingPlanID { get; set; }
        public int? ParentID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? QntIn40HC { get; set; }
        public string FactoryUD { get; set; }
    }
}