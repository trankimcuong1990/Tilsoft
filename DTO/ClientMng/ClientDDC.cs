using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientDDC
    {
        public int? DDCDetailID { get; set; }
        public int? ClientID { get; set; }
        public string Season { get; set; }

        public decimal? AllOrderTotalUSD { get; set; }
        public decimal? AllOrderTotalEUR { get; set; }
        public decimal? AllOrderConvertoEUR { get; set; }

        public decimal? ConfirmedOrderTotalUSD { get; set; }
        public decimal? ConfirmedOrderTotalEUR { get; set; }
        public decimal? ConfirmedOrderConvertoEUR { get; set; }
    }
}
