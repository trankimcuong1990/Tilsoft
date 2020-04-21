using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReportMng.DTO
{
    public class SearchForm
    {
        public List<DTO.QCReportSearchDTO> Data { get; set; }
        public List<Module.Support.DTO.Factory> Factories { get; set; }
    }
}
