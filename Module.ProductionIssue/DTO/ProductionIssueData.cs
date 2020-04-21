using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionIssue.DTO
{
    public class ProductionIssueData
    {
        public int BOMID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ProductionItemID { get; set; }
        public int? ParentBOMID { get; set; }
        public int? WorkCenterID { get; set; }

        public string WorkOrderUD { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string UnitNM { get; set; }
        public string ProductionItemTypeNM { get; set; }

        public decimal? NormQuantity { get; set; }

        public int? ToProductionTeamID { get; set; }

        public List<ProductionIssueDetailData> ProductionIssueDetail { get; set; }

        public ProductionIssueData()
        {
            ProductionIssueDetail = new List<ProductionIssueDetailData>();
        }
    }
}
