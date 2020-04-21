using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryInvoiceMng.DTO
{
    public class EditFormData
    {
        public FactoryInvoice Data { get; set; }
        public List<FactoryOrderDetailSearchResult> FactoryOrderDetails { get; set; }
        public List<FactoryOrderSparepartDetailSearchResult> FactoryOrderSparepartDetails { get; set; }
    }
}