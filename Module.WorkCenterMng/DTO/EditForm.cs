using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkCenterMng.DTO
{
    public class EditForm
    {
        public WorkCenterDto Data { get; set; }
        public List<Module.Support.DTO.Employee> Employees { get; set; }
        //public List<Module.Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }

        public List<BranchDTO> Branches { get; set; }

        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }

        public EditForm()
        {
            Branches = new List<BranchDTO>();
            FactoryWarehouses = new List<FactoryWarehouseDTO>();
        }
    }
}
