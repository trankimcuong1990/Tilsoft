using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class SearchFilterData
    {
        //public List<Module.Support.DTO.InternalCompany> Companies {get; set;}

        // Support Branch.
        public List<BranchDTO> Branches { get; set; }
        public List<Support.DTO.Employee> Employees {get; set;}

        public SearchFilterData()
        {
            Branches = new List<BranchDTO>();
            Employees = new List<Support.DTO.Employee>();
        }
    }
}
