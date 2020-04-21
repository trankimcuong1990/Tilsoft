using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MaterialOptionMng
{
    public class MaterialTestingDTO
    {
        public int MaterialTestReportID { get; set; }
        public string MaterialTestReportUD { get; set; }
        public int MaterialTestStandardID { get; set; }
        public int? MaterialOptionID { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public string TestStandardNM { get; set; }
        public List<MaterialTestingFileDTO> MaterialTestingFileDTOs { get; set; }
        public List<MaterialTestingStandardDTO> MaterialTestingStandardDTOs { get; set; }
    }
}
