using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class FactorySaleInvoiceStatusDTO
    {
        public int ConstantEntryID { get; set; }
        public int? FactorySaleInvoiceStatusID { get; set; }
        public string FactorySaleInvoiceStatusNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
