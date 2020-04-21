using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class QCReportDefect
    {
        public int? QCReportDefectID { get; set; }
        public int? QCReportID { get; set; }
        public int RowIndex { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public bool? IsFixed { get; set; }
        public bool? IsReject { get; set; }
    }
}
