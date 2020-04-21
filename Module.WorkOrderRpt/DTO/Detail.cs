using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class Detail
    {
        public int WorkOrderID { get; set; }
        public int CompanyID { get; set; }
        public int OPSequenceDetailID { get; set; }
        public int FromFactoryWarehouseID { get; set; }
        public int FromProductionTeamID { get; set; }
        public int ToFactoryWarehouseID { get; set; }
        public int ToProductionTeamID { get; set; }
        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }
        public string ProductionItemTypeNM { get; set; }
        public decimal TotalQuantity { get; set; }
        public string Unit { get; set; }
        public int InOutType { get; set; }
    }
}
