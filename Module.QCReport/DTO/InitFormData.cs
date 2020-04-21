using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QCReport.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
    }
}
