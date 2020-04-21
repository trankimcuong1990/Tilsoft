using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OutsourcingCostRpt.DTO
{
    public class Report
    {
        public Report()
        {
            ReportDataDTOs = new List<ReportDataDTO>();
        }
        public List<DTO.ReportDataDTO> ReportDataDTOs { get; set; }        
        public int? TotalRows { get; set; }
    }
}
