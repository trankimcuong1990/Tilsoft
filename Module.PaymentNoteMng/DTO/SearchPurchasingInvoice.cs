using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class SearchPurchasingInvoice
    {
        public SearchPurchasingInvoice()
        {
            Data = new List<PaymentNoteSupportSerachPurchaseInvoice>();
        }
        public int totalRows { get; set; }
        public List<DTO.PaymentNoteSupportSerachPurchaseInvoice> Data { get; set; }
    }
}
