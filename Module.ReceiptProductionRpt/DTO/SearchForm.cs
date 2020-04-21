using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ReceiptProductionRpt.DTO
{
    public class SearchForm
    {
        public List<ReceiptProduction> ReceiptProductions { get; set; }
        public List<ReceiptProductionQuantity> ReceiptProductionQuantities { get; set; }
    }
}
