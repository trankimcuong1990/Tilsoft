using System.Collections.Generic;
namespace Module.ProductionTeam.DTO
{
    public class EditData
    {
        public DTO.ProductionTeamDto Data { get; set; }
        public List<Module.Support.DTO.WorkCenter> WorkCenters { get; set; }
        public List<Module.Support.DTO.Employee> Employees { get; set; }
        public List<Module.Support.DTO.FactoryRawMaterialData> factoryRawMaterialDatas { get; set; }
    }
}
