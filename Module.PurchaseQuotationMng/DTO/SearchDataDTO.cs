using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
   public class SearchDataDTO
    {
        public List<DTO.PurchaseQuotationSearchDto> Data { get; set; }
        public List<DTO.SupportDeliveryTermDTO> supportDeliveryTermDTOs { get; set; }
        public List<DTO.SupportPaymentTermDTO> supportPaymentTermDTOs { get; set; }
        public List<DTO.SupportFactoryRawMaterialDTO> supportFactoryRawMaterialDTOs { get; set; }
    }
}
