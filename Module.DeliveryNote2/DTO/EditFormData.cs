using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DeliveryNote2.DTO
{
    public class EditFormData
    {
        public DeliveryNoteDTO Data { get; set; }
        public List<Module.Support.DTO.FactoryWarehousePallet> FactoryWarehousePallets { get; set; }
        public List<DTO.FactoryWarehouseDTO> FactoryWarehouses { get; set; }
        public List<Module.Support.DTO.ProductionTeam> ProductionTeams { get; set; }
        public List<Module.Support.DTO.OPSequenceDetail> OPSequenceDetails { get; set; }
        public List<Module.Support.DTO.ProductionItemType> ProductionItemTypes { get; set; }
        public List<BOMDTO> BOMDTOs { get; set; }
        public List<WorkCenterDTO> WorkCenters { get; set; }
        public List<Module.Support.DTO.WorkCenter> AllWorkCenters { get; set; }
        public List<SubSupplier> SubSuppliers { get; set; }
        public List<SupportFactoryWareHouseList> supportFactoryWareHouseLists { get; set; }
        public List<WorkOrderDTO> WorkOrderList { get; set; }
        public List<ListProductionItemUnit> ListProductionItemUnits { get; set; }
        public List<StatusTypeDTO> StatusTypes { get; set; }
        public List<OutsourcingCostDTO> OutsourcingCostDTOs { get; set; }
        public List<TransportationServiceDTO> TransportationServiceDTOs { get; set; }
        public List<object> ReasonOtherPrices { get; set; }
    }
}
