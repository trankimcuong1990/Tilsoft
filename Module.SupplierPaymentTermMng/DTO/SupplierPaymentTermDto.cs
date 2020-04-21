using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierPaymentTermMng.DTO
{
    public class SupplierPaymentTermDto
    {
        public int SupplierPaymentTermID { get; set; }
        public string SupplierPaymentTermNM { get; set; }
        public int? NumberDayOfPayment { get; set; }
    }
}
