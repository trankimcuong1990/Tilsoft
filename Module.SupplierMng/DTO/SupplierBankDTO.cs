using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SupplierMng.DTO
{
    public class SupplierBankDTO
    {
        public int SupplierBankID { get; set; }
        public string BankNM { get; set; }
        public string AccountNM { get; set; }
        public string BankCode { get; set; }
        public string Remark { get; set; }
        public Nullable<int> SupplierID { get; set; }
    }
}
