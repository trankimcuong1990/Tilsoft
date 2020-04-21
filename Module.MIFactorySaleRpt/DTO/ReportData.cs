using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIFactorySaleRpt.DTO
{
    public class ReportData
    {
        public List<FactorySale> FactorySales { get; set; }
        public string Season { get; set; }
    }
}
