using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class PaymentStatus
    {
        public int ConstantEntryID { get; set; }
        public int PaymentStatusID { get; set; }
        public string PaymentStatusNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}
