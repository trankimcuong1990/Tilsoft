using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PlanningProductionMng.DTO.GrantChart
{
    public class GrantChartData
    {
        public GrantChartData()
        {
            Plannings = new List<Planning>();
            ArisingByDates = new List<ArisingByDate>();
            StatisticsByDates = new List<StatisticsByDate>();
        }
        public WorkOrderInfo WorkOrderInfo { get; set; }
        public List<Planning> Plannings { get; set; }
        public List<ArisingByDate> ArisingByDates { get; set; }
        public List<StatisticsByDate> StatisticsByDates { get; set; }
    }

    public class WorkOrderInfo
    {
        public int? PlanningProductionID { get; set; }
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ProductArticleCode { get; set; }
        public string ProductDescription { get; set; }
        public int? Quantity { get; set; }
        public string WorkOrderStatusNM { get; set; }
        public string WorkOrderTypeNM { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string OPSequenceNM { get; set; }
    }

    public class Planning
    {
        public int? PlanningProductionID { get; set; }
        public int? BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ProductionTeamID { get; set; }
        public string WorkCenterNM { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public decimal? PercentComplete { get; set; }
        public decimal? PlanQnt { get; set; }
        public decimal? ReceivedQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
        public decimal? RemainQnt { get; set; }

        //tracking row field
        public int GroupIndex { get; set; }
        public bool HasBOM { get; set; }
        public int? WorkCenterIndex { get; set; }
        public long? TeamIndex { get; set; }
    }

    public class ArisingByDate
    {
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ProductionTeamID { get; set; }
        public string ArisingDate { get; set; }
        public decimal? ReceivedQnt { get; set; }
        public decimal? ProducedQnt { get; set; }
        public string ProductionTeamNM { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }

    }

    public class StatisticsByDate
    {
        public int? WorkOrderID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? ProductionTeamID { get; set; }
        public int? ProductionItemID { get; set; }
        public string ProducedDate { get; set; }
        public decimal? ProducedQnt { get; set; }
    }
}
