using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionIssue.DTO
{
    public class ProductionIssueDetailData
    {
        public int BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ParentBOMID { get; set; }
        public int? WorkCenterID { get; set; }
        public int? FromFactoryWarehouseID { get; set; }
        public int? ToProductionTeamID { get; set; }

        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public string ProductionItemTypeNM { get; set; }

        public decimal? NormQuantity { get; set; }
        public decimal? IssueQuantity { get; set; }

        public List<HistoryDeliveryNote> ProductionIssueDetailHistory { get; set; }

        public ProductionIssueDetailData()
        {
            ProductionIssueDetailHistory = new List<HistoryDeliveryNote>();
        }
    }
}
