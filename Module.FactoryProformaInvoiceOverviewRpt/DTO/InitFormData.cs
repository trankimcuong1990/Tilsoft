using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProformaInvoiceOverviewRpt.DTO
{
    public class InitFormData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.Supplier> Suppliers { get; set; }
    }
}
