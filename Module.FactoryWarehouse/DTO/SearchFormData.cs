using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class SearchFormData
    {
        public List<DTO.FactoryWarehouseSearch> Data { get; set; }

        public SearchFormData()
        {
            Data = new List<FactoryWarehouseSearch>();
        }
    }
}
