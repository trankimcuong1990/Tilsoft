using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseInvoiceMng.DTO
{
    public class PurchaseOrderSearchFromData
    {
        public PurchaseOrderSearchFromData()
        {
            Data = new List<PurchaseOrderItemDTO>();
        }
        public List<PurchaseOrderItemDTO> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
