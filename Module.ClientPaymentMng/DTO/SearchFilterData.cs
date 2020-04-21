using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DTO
{
    public class SearchFilterData
    {
        public List<Module.Support.DTO.YesNo> YesNos { get; set; }
        public List<Module.Support.DTO.ClientPaymentMethod> ClientPaymentMethods { get; set; }
    }
}
