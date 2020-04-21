using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class SearchFormData
    {
        public List<DTO.ECommercialInvoiceMng.ECommercialInvoiceSearchResult> Data { get; set; }
        public List<Support.Season> Seasons { get; set; }
        public List<DTO.ECommercialInvoiceMng.InvoiceType> InvoiceTypes { get; set; }
        public List<DTO.Support.InternalCompany3> InternalCompany3s { get; set; }

        public decimal? SumNetAmountEUR { get; set; }
        public decimal? SumVATAmountEUR { get; set; }
        public decimal? SumTotalAmountEUR { get; set; }
    }
}
