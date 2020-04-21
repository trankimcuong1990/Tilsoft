using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QAQCMng
{
    public class ReportCheckListImageDTO
    {
        public int ReportCheckListImageID { get; set; }
        public Nullable<int> ReportCheckListID { get; set; }
        public string ImageBase64 { get; set; }
        public string CheckListImageStringID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string FileName { get; set; }
    }
}
