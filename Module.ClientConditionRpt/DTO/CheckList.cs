using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientConditionRpt.DTO
{
    public class CheckList
    {
        public string Season { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ChangeField { get; set; }
        public string SaleOrderValue { get; set; }
        public string ECommercialInvoiceValue { get; set; }
    }
}
