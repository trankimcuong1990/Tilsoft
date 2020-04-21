using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CostInvoice2Mng.DTO
{
    public class EditForm
    {
        public CostInvoice2 CostInvoice2 { get; set; }
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.Currency> Currencies { get; set; }
        public List<CostInvoice2Creditor> CostInvoice2Creditors { get; set; }
        public List<CostInvoice2Type> CostInvoice2Types { get; set; }
    }
}
