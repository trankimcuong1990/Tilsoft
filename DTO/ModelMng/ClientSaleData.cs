using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ClientSaleData
    {
        public string Season { get; set; }
        public int? ModelID { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? TotalQnt { get; set; }
        public decimal? TotalCont { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
