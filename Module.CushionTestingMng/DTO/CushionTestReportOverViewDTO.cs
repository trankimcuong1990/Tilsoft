using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionTestingMng.DTO
{
    public class CushionTestReportOverViewDTO
    {
        public int CushionTestReportID { get; set; }
        public string CushionTestReportUD { get; set; }
        public int? CushionColorID { get; set; }
        public int? ClientID { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string UpdaterName { get; set; }
        public string FullName { get; set; }

        public List<CushionTestReportFileOverViewDTO> CushionTestReportFileOverViewDTOs { get; set; }
        public List<CushionTestStandardOverViewDTO> CushionTestStandardOverViewDTOs { get; set; }
    }
}
