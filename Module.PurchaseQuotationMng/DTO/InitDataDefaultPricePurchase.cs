using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class InitDataDefaultPricePurchase
    {
        public List<Support.DTO.FactoryWarehouseDto> FactoryWarehouses { get; set; }

        public List<SupportFactoryRawMaterialDTO> SubSuppliers { get; set; }

        public List<Support.DTO.YesNo> StatusDefaults { get; set; }
    }
}
