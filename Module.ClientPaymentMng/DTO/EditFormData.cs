using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DTO
{
    public class EditFormData
    {
        public DTO.ClientPayment Data { get; set; }
        public List<DTO.ECommercialInvoiceSearchResult> Invoices { get; set; }
        public List<DTO.SaleOrderSearchResult> Orders { get; set; }
        public List<DTO.SaleOrderForDeductionSearchResult> OrderForDeductions { get; set; }
    }
}
