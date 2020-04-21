using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SCMAgendaMng.DTO
{
    public class QCReportSearchFormData
    {
        public QCReportSearchFormData()
        {
            Data = new List<QCReportDTO>();
        }
        public List<QCReportDTO> Data { get; set; }
        public int TotalRows { get; set; }
        public List<Module.Support.DTO.Factory> Factories { get; set; }
    }
}
