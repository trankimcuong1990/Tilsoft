using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QAQCMng
{
    public class ReportCheckListDTO
    {
        public int ReportCheckListID { get; set; }
        public Nullable<int> ReportQAQCID { get; set; }
        public Nullable<int> CheckListID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string CheckListMobileStringID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public List<ReportCheckListImageDTO> ReportCheckListImageDTOs { get; set; }
    }
}
