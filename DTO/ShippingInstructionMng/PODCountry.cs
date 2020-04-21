using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShippingInstructionMng
{
    public class PODCountry
    {
        public int PoDID { get; set; }
        public int? ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
    }
}
