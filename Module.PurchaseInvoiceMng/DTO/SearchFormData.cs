using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class SearchFormData
    {
        public List<PurchaseInvoiceSearchDTO> Data { get; set; }
        public List<PurchaseInvoiceTypeDTO> PurchaseInvoiceTypeDTOs { get; set; }
        public List<PurchaseInvoiceStatusDTO> PurchaseInvoiceStatusDTOs { get; set; }
        public List<DTO.FactoryRawMaterialDTO> FactoryRawMaterialDTOs { get; set; }
        public TotalAmountDTO TotalAmountDTO { get; set; }

        public SearchFormData()
        {
            Data = new List<PurchaseInvoiceSearchDTO>();
            PurchaseInvoiceTypeDTOs = new List<PurchaseInvoiceTypeDTO>();
            PurchaseInvoiceStatusDTOs = new List<PurchaseInvoiceStatusDTO>();
            FactoryRawMaterialDTOs = new List<FactoryRawMaterialDTO>();
            TotalAmountDTO = new TotalAmountDTO();
        }
    }
}
