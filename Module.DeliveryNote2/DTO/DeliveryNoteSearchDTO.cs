using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class DeliveryNoteSearchDTO
    {
        public int? DeliveryNoteID { get; set; }
        public string DeliveryNoteUD { get; set; }
        public string DeliveryNoteDate { get; set; }
        public string WorkCenterNM { get; set; }
        public string FromProductionTeamNM { get; set; }
        public string ToProductionTeamNM { get; set; }
        public string SaleOrderNo { get; set; }
        public string Description { get; set; }
        public int? CompanyID { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string ViewName { get; set; }
        public int? DeliveryNoteTypeID { get; set; }
        public int? CreatedBy { get; set; }
        public List<WorkOrderSearchDTO> WorkOrderSearchDTOs { get; set; }
        public List<FactoryWareHouseSearchDTO> factoryWareHouseSearchDTOs { get; set; }
        public string WorkOrderIDs { get; set; }
        public bool? IsApproved { get; set; }
        public int? StatusTypeID { get; set; }
        public string StatusTypeNM { get; set; }
    }
    public class WorkOrderSearchDTO
    {
        public int? WorkOrderID { get; set; }
        public string WorkOrderUD { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
    }

    public class FactoryWareHouseSearchDTO
    {
        public int? DeliveryNoteID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public string FactoryWarehouseUD { get; set; }
        public string FactoryWarehouseNM { get; set; }
    }
}
