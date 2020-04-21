using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QAQCMng
{
    public class ReportMobileInputDTO
    {
        public List<ReportQAQCDTO> ReportQAQCDTOs { get; set; }
        public string StrUserID { get; set; }
    }
}
