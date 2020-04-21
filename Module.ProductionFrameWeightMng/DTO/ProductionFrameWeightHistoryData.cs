using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionFrameWeightMng.DTO
{
    public class ProductionFrameWeightHistoryData
    {
        public int ProductionFrameWeightHistoryID { get; set; }
        public int? ProductionFrameWeightID { get; set; }
        public decimal? FrameWeight { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public int RowIndex { get; set; }
    }
}
