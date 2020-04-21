using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class EditDataDTO
    {
        public PurchaseQuotationDTO Data { get; set; }
        public List<DTO.SupportDeliveryTermDTO> supportDeliveryTermDTOs { get; set; }
        public List<DTO.SupportPaymentTermDTO> supportPaymentTermDTOs { get; set; }
        public List<DTO.SupportFactoryRawMaterialDTO> supportFactoryRawMaterialDTOs { get; set; }
    }
}
