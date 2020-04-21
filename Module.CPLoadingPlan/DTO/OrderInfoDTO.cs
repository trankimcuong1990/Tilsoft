using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CPLoadingPlan.DTO
{
    public class OrderInfoDTO
    {
        public int? LoadingPlanID { get; set; }
        public int SaleOrderID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ClientOrderNumber { get; set; }
    }
}
