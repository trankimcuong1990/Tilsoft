using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseTransportMng
{
    public class EditSupportList
    {
        public List<DTO.Support.User> Users { get; set; }
        public List<DTO.WarehouseTransportMng.PhysicalStockByWarehouseArea> FromWarehouseAreas { get; set; }
        public List<DTO.Support.WareHouseArea> ToWarehouseAreas { get; set; }
        public List<DTO.Support.ProductStatus> ProductStatuses { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
    }
}
