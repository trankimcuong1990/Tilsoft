using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WarehouseTransferMng.DTO
{
    public class EditFormData
    {
        public WarehouseTransferDTO Data { get; set; }
        public List<Module.Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }
       
    }
}
