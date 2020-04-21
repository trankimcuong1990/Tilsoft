using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductWizardMng.DTO
{
    public class ClientSpecialPackagingMethodDTO
    {
        public int ClientSpecialPackagingMethodID { get; set; }
        public string ClientSpecialPackagingMethodNM { get; set; }
        public string Description { get; set; }
        public Nullable<int> ClientID { get; set; }
    }
}
