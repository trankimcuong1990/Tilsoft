using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QAQCMng
{
    public class ReportDefectDTO
    {
        public int ReportDefectID { get; set; }
        public Nullable<int> ReportQAQCID { get; set; }
        public Nullable<int> DefectID { get; set; }
        public Nullable<int> DefectAQty { get; set; }
        public Nullable<int> DefectBQty { get; set; }
        public Nullable<int> DefectCQty { get; set; }
        public string DefectMobileStringID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public List<DTO.QAQCMng.ReportDefectImageDTO> ReportDefectImageDTOs { get; set; }
    }
}
