using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionTestingMng.DTO
{
    public class CushionTestReportSearchDTO
    {
        public int? CushionTestReportID { get; set; }
        public string CushionTestReportUD { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }

        public bool? IsEnabled { get; set; }
        public string CushionColorUD { get; set; }
        public string CushionColorNM { get; set; }

        public string ClientUD { get; set; }
        public string ClientNM { get; set; }

        public string FileUD { get; set; }

        public List<DTO.CushionTestFileSearchDTO> CushionTestFileSearchDTOs { get; set; }
        public List<DTO.CushionTestStandardSearchDTO> CushionTestStandardSearchDTOs { get; set; }
    }
}
