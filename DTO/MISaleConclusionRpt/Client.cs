using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleConclusionRpt
{
    public class Client
    {
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? ClientCountryID { get; set; }
    }
}
