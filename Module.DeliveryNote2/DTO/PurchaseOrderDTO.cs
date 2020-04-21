using System.Collections.Generic;

namespace Module.DeliveryNote2.DTO
{
    public class PurchaseOrderDTO
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderUD { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierShortNM { get; set; }
        public string SupplierFullNM { get; set; }
        public string SupplierAddress { get; set; }
        public List<PurchaseOrderDetailDTO> PurchaseOrderDetails { get; set; }

        public PurchaseOrderDTO()
        {
            PurchaseOrderDetails = new List<PurchaseOrderDetailDTO>();
        }
    }
}
