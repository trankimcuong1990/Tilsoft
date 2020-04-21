using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionIssue.DTO
{
    public class DeliveryNote
    {
        public int DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string DeliveryNoteDate { get; set; }
        public int? OPSequenceDetailID { get; set; }
        public int? FromFactoryWarehouseID { get; set; }
        public int? FromProductionTeamID { get; set; }
        public int? ToProductionTeamID { get; set; }
        public string SaleOrderNo { get; set; }
        public int? WorkOrderID { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string ViewName { get; set; }

        public List<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
    }
}
