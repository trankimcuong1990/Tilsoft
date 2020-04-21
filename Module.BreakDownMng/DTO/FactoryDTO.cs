using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class FactoryDTO
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public decimal? ExportCost20DC { get; set; }
        public decimal? ExportCost40DC { get; set; }
        public decimal? ExportCost40HC { get; set; }
    }
}
