using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class PurchaseRequestStatusDTO
    {
        public int PurchaseRequestStatusID { get; set; }
        public string PurchaseRequestStatusNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
