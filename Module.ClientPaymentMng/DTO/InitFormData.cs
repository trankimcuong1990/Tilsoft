using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientPaymentMng.DTO
{
    public class InitFormData
    {
        public List<Module.Support.DTO.Client> Clients { get; set; }
        public List<Module.Support.DTO.ClientPaymentMethod> ClientPaymentMethods { get; set; }
    }
}
