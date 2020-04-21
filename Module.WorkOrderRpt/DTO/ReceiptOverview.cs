using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class ReceiptOverview
    {
        public int ReceiptID { get; set; }
        public string ReceiptUD { get; set; }
        public string ReceiptDate { get; set; }
        public string FromFactoryWarehouseNM { get; set; }
        public string FromProductionTeamNM { get; set; }
        public string ToFactoryWarehouseNM { get; set; }
        public string ToProductionTeamNM { get; set; }
        public string Description { get; set; }
        public int CompanyID { get; set; }
        public int WorkOrderID { get; set; }
        public int ReceiptType { get; set; }
    }
}
