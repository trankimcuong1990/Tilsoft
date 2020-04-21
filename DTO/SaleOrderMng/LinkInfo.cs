using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SaleOrderMng
{
    public class LinkInfo
    {
        public int OfferID { get; set; }
        public int? ClientID { get; set; }
        public string Season { get; set; }
        public string Currency { get; set; }
    }
}
