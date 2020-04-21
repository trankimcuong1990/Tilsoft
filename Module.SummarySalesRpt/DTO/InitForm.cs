using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SummarySalesRpt.DTO
{
    public class InitForm
    {
        public List<DTO.SupportCustomerData> SupportCustomer { get; set; }
        public List<DTO.DeliveryNote> DeliveryNotes { get; set; }
    }
}
