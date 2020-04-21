using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoice2Mng.DTO
{
    public class SearchFormData
    {
        public List<DTO.FactoryProformaInvoiceSearchResult> Data { get; set; }
        public DTO.SummaryRow SummaryData { get; set; }
    }
}
