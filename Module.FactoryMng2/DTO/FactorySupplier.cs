using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactorySupplier
    {
        public int FactorySupplierID { get; set; }
        public int? FactoryID { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierNM { get; set; }
        public string SupplierUD { get; set; }
    }
}
