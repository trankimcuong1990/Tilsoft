using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class LogInspectorDTO
    {
        public string SysDate { get; set; }
        public Nullable<int> QAQCID { get; set; }
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int FactoryOrderID { get; set; }
        public string FactoryOrderUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public int FactoryOrderDetailID { get; set; }
        public string ApprovalNM { get; set; }
        public string InspectorNM { get; set; }
        public Nullable<int> ReportQAQCID { get; set; }
    }
}
