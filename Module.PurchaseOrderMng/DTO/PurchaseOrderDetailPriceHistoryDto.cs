using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseOrderMng.DTO
{
    public class PurchaseOrderDetailPriceHistoryDto
    {
        public int PurchaseOrderDetailPriceHistoryID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public decimal UnitPrice { get; set; }
        public int ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string EmployeeNM { get; set; }
    }
}
