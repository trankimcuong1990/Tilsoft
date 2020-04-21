using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class SupportInspectionStage
    {
        public int ConstantEntryID { get; set; }
        public int? InspectionStageID { get; set; }
        public string InspectionStageNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
