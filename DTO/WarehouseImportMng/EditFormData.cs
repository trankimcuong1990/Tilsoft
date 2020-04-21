using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WarehouseImportMng
{
    public class EditFormData
    {
        public WarehouseImportMng.WarehouseImport Data { get; set; }
        public List<Support.ProductStatus> ProductStatuses { get; set; }
        public List<DTO.Support.Season> Season { get; set; }
        public List<DTO.Support.WareHouseArea> WareHouseAreas { get; set; }
        public List<DTO.WarehouseImportMng.ProductColli> ProductCollis { get; set; }
    }
}
