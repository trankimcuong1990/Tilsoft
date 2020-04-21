using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class POD
    {
        public int PODID { get; set; }
        public string PODUD { get; set; }
        public string PODName { get; set; }
        public int? ClientCountryID { get; set; }
        public string ClientCountryNM { get; set; }

    }
}
