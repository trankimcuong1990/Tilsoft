using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTestingMng.DTO
{
    public class MaterialTestReportDTO
    {
        public int? MaterialTestReportID { get; set; }
        public string MaterialTestReportUD { get; set; }
        public int? MaterialOptionID { get; set; }
        public string MaterialOptionUD { get; set; }
        public string MaterialOptionNM { get; set; }
        public int? MaterialTestStandardID { get; set; }
        public string MaterialTestStandardNM { get; set; }
        public string TestDate { get; set; }
        public string Remark { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdaterName { get; set; }


        public List<MaterialTestReportFileDTO> MaterialTestReportFileDTOs { get; set; }
        public List<MaterialTestStandardDTO> MaterialTestStandardDTOs { get; set; }
    }
}
