using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseQuotationMng.DTO
{
    public class InitDefaultPriceDTO
    {
        public List<DTO.PurchaseQuotationDTO> Data { get; set; }
        public List<DTO.SupplierDTO> Suppliers { get; set; }
    }
}
