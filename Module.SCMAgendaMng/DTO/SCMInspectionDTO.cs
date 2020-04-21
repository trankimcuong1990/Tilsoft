using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class SCMInspectionDTO
    {
        public int SCMInspectionID { get; set; }
        public Nullable<int> SCMAppointmentID { get; set; }
        public Nullable<int> QCReportID { get; set; }
        public string Remark { get; set; }
        public string QCReportUD { get; set; }
        public string InspectedDate { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string QCName { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string ReportFile { get; set; }
        public string ReportFileLocation { get; set; }
        public string ReportFriendlyName { get; set; }
    }
}
