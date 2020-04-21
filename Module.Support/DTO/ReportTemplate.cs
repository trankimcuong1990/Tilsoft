using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class ReportTemplate
    {
        public int? ReportTemplateID { get; set; }
        public string ReportTemplateNM { get; set; }
        public string ReportType { get; set; }
        public bool IsDefault { get; set; }
    }
}
