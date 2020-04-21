using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummaryOutWardRpt.DTO
{
    public class SummaryOutWardReportData
    {
        public int BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? CompanyID { get; set; }
        public int? ParentBOMID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public decimal? BOMQnt { get; set; }
        public decimal? ActualQnt { get; set; }
        public decimal? TotalAmountBOM { get; set; }
        public decimal? TotalAmountActual { get; set; }
        public decimal? DiffirenceQnt { get; set; }
        public decimal? TotalAmountDiffirence { get; set; }
        public int? MonthFinishDate { get; set; }
        public int? YearFinishDate { get; set; }
        public string WorkOrderUD { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public int? ProductionItemID { get; set; }
        public decimal? QtyByUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalValue { get; set; }
        public int? ProductionItemTypeID { get; set; }
        public decimal? DiffirenceQntOfComponent { get; set; }
        public decimal? TotalDiffirenceOfComponent { get; set; }
    }
}
