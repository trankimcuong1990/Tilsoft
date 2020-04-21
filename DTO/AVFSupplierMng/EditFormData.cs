using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AVFSupplierMng
{
    public class EditFormData
    {
        public DTO.AVFSupplierMng.AVFSupplier Data { get; set; }
        public List<DTO.Support.LedgerAccount> LedgerAccounts { get; set; }
    }
}
