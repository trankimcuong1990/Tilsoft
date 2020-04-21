using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class PIContainerPrintout
    {
        public List<PIPrintout> PIPrintouts { get; set; }
        public List<PIDetailPrintout> PIDetailPrintouts { get; set; }
        public string ReportName { get; set; }
    }
}
