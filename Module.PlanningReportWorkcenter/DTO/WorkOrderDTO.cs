using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningReportWorkcenter.DTO
{
    public class WorkOrderDTO
    {
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientUD { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FileLocation { get; set; }
        public string ModelNM { get; set; }
        public int? Qnt40HC { get; set; }
        public int? Quantity { get; set; }
        public decimal? OrderCont { get; set; }
        public string LDS { get; set; }
        public decimal? PlanQnt { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string WeekStart { get; set; }
        public string WeekEnd { get; set; }
        public string WorkOrderStatus { get; set; }
        public string WorkOrderUD { get; set; }
        public int? Duration { get; set; }
        public int? WeekInfoID { get; set; }
        public int? CompanyID { get; set; }
        public string ImageFile { get; set; }
        public int? PlanningProductionGrantChartID { get; set; }
        public int? PieceQnt { get; set; }
        public int? Remain { get; set; }
        public string FrameStatus { get; set; }
        public string WeavingStatus { get; set; }
        public string PackingStatus { get; set; }
        public string CushionStatus { get; set; }
        public string WoodStatus { get; set; }
        public string CartonStatus { get; set; }

    }
}
