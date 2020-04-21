using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class EditFormData
    {
        public EditFormData()
        {
            Data = new PurchaseInvoiceDTO();
            PurchaseInvoiceTypeDTOs = new List<PurchaseInvoiceTypeDTO>();
            PurchaseInvoiceStatusDTOs = new List<PurchaseInvoiceStatusDTO>();
            FactoryRawMaterialDTOs = new List<FactoryRawMaterialDTO>();
            FactoryWarehouseDTOs = new List<FactoryWarehouseDTO>();
        }
        public PurchaseInvoiceDTO Data { get; set; }
        public List<DTO.PurchaseInvoiceTypeDTO> PurchaseInvoiceTypeDTOs { get; set; }
        public List<DTO.PurchaseInvoiceStatusDTO> PurchaseInvoiceStatusDTOs { get; set; }
        public List<DTO.FactoryRawMaterialDTO> FactoryRawMaterialDTOs { get; set; }
        public List<DTO.FactoryWarehouseDTO> FactoryWarehouseDTOs { get; set; }
        public List<DTO.PaymentTermDTO> PaymentTermDTOs { get; set; }
    }
}
