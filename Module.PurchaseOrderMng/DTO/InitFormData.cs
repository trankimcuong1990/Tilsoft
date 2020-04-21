using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class InitFormData
    {
        // Get support list FactoryRawMaterial
        public List<FactoryRawMaterialDto> SubSupplier { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
