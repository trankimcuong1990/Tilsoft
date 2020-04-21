using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class QAQCReportStarusDTO
    {
        public int ReportQAQCID { get; set; }
        public Nullable<int> QAQCID { get; set; }
        public Nullable<int> TypeOfInspectionID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string ManagementStringID { get; set; }

    }
}
