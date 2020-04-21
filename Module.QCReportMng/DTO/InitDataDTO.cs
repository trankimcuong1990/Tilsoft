using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class InitDataDTO
    {
        public List<Module.Support.DTO.Factory> Factories { get; set; }
        public List<DTO.SupportQCReportSetting> Data { get; set; }
    }
}
