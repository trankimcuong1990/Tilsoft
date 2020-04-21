using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class QCReportTestEnvironmentDTO
    {
        public int QCReportTestEnvironmentID { get; set; }
        public int? QCTestReportID { get; set; }
        public int? QCReportTestEnvironmentItemID { get; set; }
        public bool? IsSelected { get; set; }
        public string QCReportTestEnvironmentItemNM { get; set; }
        public int? QCReportTestEnvironmentCategoryID { get; set; }
        public string QCReportTestEnvironmentCategoryNM { get; set; }
    }
}
