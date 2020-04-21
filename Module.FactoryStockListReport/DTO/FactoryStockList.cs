using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStockListReport.DTO
{
    public class FactoryStockList
    {
        public object KeyID { get; set; }
        public int? FactoryID { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FactoryUD { get; set; }
        public string ClientUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string LDS { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? Qnt40HC { get; set; }
        public int? TotalStockQnt { get; set; }
        public decimal? TotalStockQntIn40HC { get; set; }
        public int? TotalProducedQnt { get; set; }
        public int? TotalNotPackedQnt { get; set; }
        public int? TotalPackedQnt { get; set; }
    }
}
