using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class ClientSpecialPackagingMethod
    {
        public int ClientSpecialPackagingMethodID { get; set; }
        public string ClientSpecialPackagingMethodNM { get; set; }
        public string Description { get; set; }
        public int? ClientID { get; set; }
    }
}
