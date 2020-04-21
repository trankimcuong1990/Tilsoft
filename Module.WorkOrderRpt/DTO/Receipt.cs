using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrderRpt.DTO
{
    public class Receipt
    {
        public int WorkOrderID { get; set; }
        public int CompanyID { get; set; }
        public int OPSequenceDetailID { get; set; }
        public int ReceivingNoteID { get; set; }
        public string ReceivingNoteUD { get; set; }
        public int DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string ReceiptDate { get; set; }
        public int FromFactoryWarehouseID { get; set; }
        public int FromProductionTeamID { get; set; }
        public int ToFactoryWarehouseID { get; set; }
        public int ToProductionTeamID { get; set; }
        public int ProductionItemID { get; set; }
        public int InOutType { get; set; }
    }
}
