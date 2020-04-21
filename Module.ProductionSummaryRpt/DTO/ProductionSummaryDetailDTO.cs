using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionSummaryRpt.DTO
{
    public class ProductionSummaryDetailDTO
    {
        public int? ProductID { get; set; }
        public int? WorkCenterID { get; set; }
        public decimal? TotalImportQnt { get; set; }
        public decimal? TotalFinishQnt { get; set; }
        public decimal? TotalExportQnt { get; set; }
        public decimal? TotalRemainQnt { get; set; }
        public int PrimaryID { get; set; }
        public decimal? TotalImport40HC { get; set; }
        public decimal? TotalFinish40HC { get; set; }
        public decimal? TotalExport40HC { get; set; }
        public decimal? TotalRemain40HC { get; set; }
    }
}
