using System.Collections.Generic;

namespace Module.StorageCardRpt.DTO
{
    public class InitNewForm
    {
        public ProductionItem Data { get; set; }

        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }
    }
}
