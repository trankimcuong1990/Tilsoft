using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DTO
{
    public class InitForm
    {
        public List<Support.DTO.Employee> Data { get; set; }

        public List<BranchDTO> Branches { get; set; }

        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }

        public InitForm()
        {
            Branches = new List<BranchDTO>();
            FactoryWarehouses = new List<FactoryWarehouseDTO>();
        }
    }
}
