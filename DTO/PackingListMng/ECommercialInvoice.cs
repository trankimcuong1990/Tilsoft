using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PackingListMng
{
    public class ECommercialInvoice
    {
        public int ECommercialInvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public int PackingListID { get; set; }
    }
}
