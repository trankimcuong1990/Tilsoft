using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehousePickingListMng
{
    public class EditFormData
    {
        public WarehousePickingList Data { get; set; }
        public List<Support.User> Users { get; set; }
        public List<Support.PackagingMethod> PackagingMethods { get; set; }
        public List<DTO.Support.Season> Seasons { get; set; }
        public List<DTO.WarehousePickingListMng.PhysicalStockByWarehouseArea> PhysicalStockByWarehouseAreas { get; set; }
    }
}
