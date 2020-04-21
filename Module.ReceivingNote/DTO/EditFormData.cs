using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceivingNote.DTO
{
    public class EditFormData
    {
        public ReceivingNoteDTO Data { get; set; }
        public List<Module.Support.DTO.FactoryWarehousePallet> FactoryWarehousePallets { get; set; }
        //public List<Module.Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }
        public List<Module.Support.DTO.ProductionTeam> ProductionTeams { get; set; }
        public List<Module.Support.DTO.OPSequenceDetail> OPSequenceDetails { get; set; }
        public List<Module.Support.DTO.ProductionItemType> ProductionItemTypes { get; set; }
        public List<BOMDTO> BOMDTOs { get; set; }

        public List<DTO.WorkCenter> WorkCenters { get; set; }

        public List<DTO.SubSupplier> SubSuppliers { get; set; }
        public List<DTO.WorkOrderDTO> WorkOrderList { get; set; }

        public List<StatusTypeDTO> StatusTypes { get; set; }

        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }
        public List<TransportationServiceDTO> TransportationServiceDTOs { get; set; }
        public List<object> ReasonOtherPrices { get; set; }

        public EditFormData()
        {
            FactoryWarehouses = new List<FactoryWarehouseDTO>();
        }
    }
}
