using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class SearchFormData
    {
        public List<WarehouseTransferSearchDTO> Data { get; set; }
        public List<ShowFactoryhouseNM> showFactoryhouseNMs { get; set; }
        //public List<Module.Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }
        public List<FactoryWarehouseDTO> FactoryWarehouses { get; set; }
    }
}
