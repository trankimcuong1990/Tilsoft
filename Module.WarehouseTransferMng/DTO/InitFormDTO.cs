using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class InitFormDTO
    {
        public List<BranchDTO> Branches { get; set; }

        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }

        public InitFormDTO()
        {
            Branches = new List<BranchDTO>();
            FactoryWarehouses = new List<FactoryWarehouseDTO>();
        }
    }
}
