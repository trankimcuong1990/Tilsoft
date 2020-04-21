using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class QCReportDefectDTO
    {
        public int QCReportDefectID { get; set; }
        public int? QCReportID { get; set; }
        public string Description { get; set; }
        public int? Critical { get; set; }
        public decimal? Major { get; set; }
        public decimal? Minor { get; set; }
        public string Remark { get; set; }
    }
}
