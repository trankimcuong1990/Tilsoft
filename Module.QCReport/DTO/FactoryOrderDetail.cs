using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class FactoryOrderDetail
    {
        public int? FactoryOrderDetailID { get; set; }
        public int OrderQnt { get; set; }
        public string Season { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string FactoryUD { get; set; }
        public int? FactoryID { get; set; }
    }
}
