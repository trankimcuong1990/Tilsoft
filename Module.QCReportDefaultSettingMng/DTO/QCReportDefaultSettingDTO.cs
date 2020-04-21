using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportDefaultSettingMng.DTO
{
    public class QCReportDefaultSettingDTO
    {
        public int QCReportDefaultSettingID { get; set; }
        public string QCReportSection { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public int? RowIndex { get; set; }
        public bool? IsRetrievableFromModel { get; set; }
        public int? ModelConnectorCodeID { get; set; }
    }
}
