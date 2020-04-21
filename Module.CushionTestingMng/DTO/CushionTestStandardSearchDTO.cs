using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionTestingMng.DTO
{
    public class CushionTestStandardSearchDTO
    {
        public int CushionTestReportUsingCushionStandardID { get; set; }
        public int? CushionTestReportID { get; set; }
        public int? CushionTestStandardID { get; set; }
        public string TestStandardNM { get; set; }
    }
}
