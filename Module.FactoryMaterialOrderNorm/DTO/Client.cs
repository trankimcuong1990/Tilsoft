using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialOrderNorm.DTO
{
    public class Client
    {
        public object KeyID { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string Season { get; set; }
        public List<ClientProduct> ClientProducts { get; set; }

    }
}
