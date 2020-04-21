using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStockReceipt.DTO
{
    public class SearchFormData
    {
        public List<FactoryStockReceiptSearch> Data { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
    }
}
