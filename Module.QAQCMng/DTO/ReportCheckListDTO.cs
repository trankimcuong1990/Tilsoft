using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class ReportCheckListDTO
    {
        public ReportCheckListDTO()
        {
            reportCheckListImages = new List<ReportCheckListImageDTO>();
        }
        public int ReportCheckListID { get; set; }
        public Nullable<int> ReportQAQCID { get; set; }
        public Nullable<int> CheckListID { get; set; }
        public string Remark { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string StatusNM { get; set; }
        public string CheckListMobileStringID { get; set; }
        public string CheckListUD { get; set; }
        public string CheckListNM { get; set; }

        public List<QAQCMng.DTO.ReportCheckListImageDTO> reportCheckListImages { get; set; }
    }
}
