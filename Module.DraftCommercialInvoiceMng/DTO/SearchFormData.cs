using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftCommercialInvoiceMng.DTO
{
    public class SearchFormData
    {
        public List<DTO.DraftCommercialInvoiceSearchDTO> Data { get;set; }
        public List<DTO.Season> Seasons { get; set; }
    }
}
