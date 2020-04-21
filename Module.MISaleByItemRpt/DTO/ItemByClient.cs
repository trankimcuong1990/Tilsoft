using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MISaleByItemRpt.DTO
{
    public class ItemByClient
    {
        public string Season { get; set; }
        public int? ModelID { get; set; }
        
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        
        public int? ClientCountryID { get; set; }
        public string ClientCountryNM { get; set; }
        public string ClientCountryUD { get; set; }

        public int? SaleID { get; set; }
        public string SaleUD { get; set; }
        public string SaleNM { get; set; }

    }
}
