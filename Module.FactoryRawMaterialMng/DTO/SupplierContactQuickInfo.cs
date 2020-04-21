using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class SupplierContactQuickInfo
    {
        public int SupplierContactQuickInfoId { get; set; }
        public Nullable<int> FactoryRawMaterialID { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
    }
}
