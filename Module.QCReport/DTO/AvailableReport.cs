using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class AvailableReport
    {
        public int? QCReportID { get; set; }
        public int? FactoryOrderDetailID { get; set; }
        public string QCReportNM { get; set; }
        public string Remark { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string Season { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }

        public string FileUD { get; set; }
    }
}
