using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CushionTestingMng.DTO
{
    public class EditFormData
    {
        public CushionTestReportDTO Data { get; set; }
        public List<SupportCushionTestStandardDTO> SupportCushionTestStandards { get; set; }
        public CushionTestReportOverViewDTO DataOverView { get; set; }
    }
}
