using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class EditFormData
    {
        public EditFormData()
        {
            Data = new FactorySaleInvoiceDTO();
            FactorySaleInvoiceStatus = new List<FactorySaleInvoiceStatusDTO>();
            FactoryRawMaterialDTOs = new List<FactoryRawMaterialDTO>();
            Seasons = new List<Support.DTO.Season>();
        }
        public FactorySaleInvoiceDTO Data { get; set; }
        public List<FactorySaleInvoiceStatusDTO> FactorySaleInvoiceStatus { get; set; }
        public List<FactoryRawMaterialDTO> FactoryRawMaterialDTOs { get; set; }
        public List<PaymentTermDTO> PaymentTermDTOs { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
