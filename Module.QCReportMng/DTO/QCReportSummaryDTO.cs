using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class QCReportSummaryDTO
    {
        public int QCReportSummaryID { get; set; }
        public int? QCReportID { get; set; }
        public string Description { get; set; }
        public int? QCReportSummaryResultID { get; set; }
        public string Remark { get; set; }
        public string QCReportSummaryResultNM { get; set; }
    }
}
