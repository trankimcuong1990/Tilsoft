using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TransportInvoice.DTO
{
    public class SearchFormData
    {
        public List<TransportInvoiceSearchDTO> Data { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
    }
}
