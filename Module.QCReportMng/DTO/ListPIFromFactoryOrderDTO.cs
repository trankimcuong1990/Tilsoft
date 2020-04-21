using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class ListPIFromFactoryOrderDTO
    {
        public int FactoryOrderDetailID { get; set; }
        public Nullable<int> FactoryOrderID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> SaleOrderDetailID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public Nullable<int> OrderQnt { get; set; }
        public string AdditionalRemark { get; set; }
        public string ProductionStatus { get; set; }
        public string LDS { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}
