using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MaterialTestingMng.DTO
{
    public class EditFormData
    {
        public MaterialTestReportDTO Data { get; set; }
        public List<SupportMaterialTestStandardDTO> SupportMaterialTestStandards { get; set; }
        public MaterialTestReportOverViewDTO DataOverView { get; set; }
    }
}
