using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ChartCommercialInvoice
    {
        public int KeyID { get; set; }
        public string ClientNM { get; set; }
        public string Season { get; set; }
        public decimal? TotalEURAmount { get; set; }
    }
}
