using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class SupportQCReportSetting
    {
        public int SaleOrderDetailID { get; set; }
        public int? SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public int? Quantity { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
    }
}
