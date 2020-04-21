using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2Mng.DTO
{
    public class SearchFormData
    {
        public List<FactoryPaymentSearchResult> Data { get; set; }
        public DTO.SummaryRow SummaryData { get; set; }
    }
}
