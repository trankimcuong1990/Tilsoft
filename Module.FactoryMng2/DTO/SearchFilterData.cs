using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.Employee> Employees { get; set; }
        public List<DTO.FactoryRawMaterialSupplier> FactoryRawMaterialSupplierList { get; set; }
        //public List<DTO.Supplier> SupplierList { get; set; }
        public List<Support.DTO.FactoryLocation> Locations { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
    }
}
