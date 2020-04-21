using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class ReportDefectDTO
    {
        public ReportDefectDTO()
        {
            reportDefectsImageDTOs = new List<ReportDefectsImageDTO>();
        }
        public int ReportDefectID { get; set; }
        public Nullable<int> ReportQAQCID { get; set; }
        public Nullable<int> DefectID { get; set; }
        public Nullable<int> DefectAQty { get; set; }
        public Nullable<int> DefectBQty { get; set; }
        public Nullable<int> DefectCQty { get; set; }
        public string DefectUD { get; set; }
        public string DefectNM { get; set; }
        public string DefectAUD { get; set; }
        public string DefectANM { get; set; }
        public string DefectBUD { get; set; }
        public string DefectBNM { get; set; }
        public string DefectCUD { get; set; }
        public string DefectCNM { get; set; }

        public List<DTO.ReportDefectsImageDTO> reportDefectsImageDTOs { get; set; }
    }
}
