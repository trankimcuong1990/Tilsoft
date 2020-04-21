using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LedgerAccountMng.DTO
{
    public class EditFormData
    {
        public Module.LedgerAccountMng.DTO.LedgerAccount Data { get; set; }
        public List<Module.Support.DTO.LedgerAccount> LedgerAccounts { get; set; }
    }
}
