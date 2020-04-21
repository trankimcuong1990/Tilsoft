using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CushionColorMng
{
    public class CushionTestingDTO
    {
        public int CushionTestReportID { get; set; }
        public string CushionTestReportUD { get; set; }
        public int CushionTestStandardID { get; set; }
        public int? CushionOptionID { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string TestStandardNM { get; set; }
        public List<CushionTestingFileDTO> CushionTestingFileDTOs { get; set; }
        public List<CushionTestingStandardDTO> CushionTestingStandardDTOs { get; set; }
    }
}
