using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShowroomRpt.DTO
{
    public class InitDto
    {
        public List<Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }
        public List<DTO.FactoryWarehouse> Data { get; set; }
        public List<DTO.ReceivingNote> ReceivingNotes { get; set; }
        public List<DTO.FactoryWarehousePallet> FactoryWarehousePallets { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
