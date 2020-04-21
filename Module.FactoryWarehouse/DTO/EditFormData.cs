using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class EditFormData
    {
        public FactoryWarehouse Data { get; set; }
        public List<PalletOverview> PalletOverviews { get; set; }
        public List<CapacityOverview> CapacityOverviews { get; set; }
        public List<BranchDTO> Branches { get; set; }
        public List<Support.DTO.Employee> Employees{get; set;}

        public EditFormData()
        {
            Data = new FactoryWarehouse();
            PalletOverviews = new List<PalletOverview>();
            CapacityOverviews = new List<CapacityOverview>();
            Branches = new List<BranchDTO>();
            Employees = new List<Support.DTO.Employee>();
        }
    }
}
