using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFSupplierMng.DTO
{
    public class EditFormData
    {
        public Module.AVFSupplierMng.DTO.AVFSupplier Data { get; set; }
        public List<Module.Support.DTO.LedgerAccount> LedgerAccounts { get; set; }
    }
}
