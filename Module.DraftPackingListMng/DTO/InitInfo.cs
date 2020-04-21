using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng.DTO
{
    public class InitInfo
    {
        public int DraftCommercialInvoiceID { get; set; }
        public string Season { get; set; }
        public string ClientUD { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public int? AccountNo { get; set; }
        public string RefNo { get; set; }
        public string LCRefNo { get; set; }
        public string Conditions { get; set; }
        public string Remark { get; set; }
    }
}
