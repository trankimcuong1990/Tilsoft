using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class SupportInspectionResult
    {
        public int ConstantEntryID { get; set; }
        public int? InspectionResultID { get; set; }
        public string InspectionResultNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
