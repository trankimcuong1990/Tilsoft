using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class EditData
    {
        public PurchaseOrderDto Data { get; set; }
        public decimal? TotalAmountValue { get; set; }
        public List<FactoryRawMaterialDto> FactoryRawMaterials { get; set; }
        //public List<PurchaseOrderRequestingItemDto> PurchaseOrderRequestingItemDtos { get; set; }
        public List<SupportPurchaseRequestDto> PurchaseRequests { get; set; }
        public List<SupplierPaymentTermDto> SupplierPaymentTermDtos { get; set; }
    }
}
