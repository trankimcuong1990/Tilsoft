using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QAQCMng.DTO
{
    public class ReportDataDTO
    {
        public ReportDataDTO()
        {
            reportQAQCSearchDTOs = new List<ReportQAQCSearchDTO>();
        }
        public int totalRows { get; set; }
        public List<ReportQAQCSearchDTO> reportQAQCSearchDTOs { get; set; }
    }
}
