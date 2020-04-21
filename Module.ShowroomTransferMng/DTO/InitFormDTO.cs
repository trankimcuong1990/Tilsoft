using System.Collections.Generic;

namespace Module.ShowroomTransferMng.DTO
{
    public class InitFormDTO
    {
        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }
        public List<FactoryWarehousePalletDTO> FactoryWarehousePallets { get; set; }

        public InitFormDTO()
        {
            FactoryWarehouses = new List<FactoryWarehouseDTO>();
            FactoryWarehousePallets = new List<FactoryWarehousePalletDTO>();
        }
    }
}
