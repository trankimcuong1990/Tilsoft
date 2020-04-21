using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ReportTemplate
    {
        public int? ReportTemplateID { get; set; }
        public string ReportTemplateNM { get; set; }
        public string ReportType { get; set; }
        public bool IsDefault { get; set; }
    }
}
