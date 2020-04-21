using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class QCReportTestEnvironmentItemDTO
    {
        public int QCReportTestEnvironmentItemID { get; set; }
        public int? QCReportTestEnvironmentCategoryID { get; set; }
        public string Description { get; set; }
    }
}
