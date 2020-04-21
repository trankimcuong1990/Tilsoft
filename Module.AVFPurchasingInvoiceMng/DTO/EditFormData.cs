using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFPurchasingInvoiceMng.DTO
{
    public class EditFormData
    {
        public Module.AVFPurchasingInvoiceMng.DTO.AVFPurchasingInvoice Data { get; set; }
        public List<Module.Support.DTO.LedgerAccount> LedgerAccounts { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
    }
}
