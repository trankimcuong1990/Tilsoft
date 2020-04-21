using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleConclusionRpt
{
    public class ExpectedByClient
    {
        public int ClientID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public Nullable<int> ClientCountryID { get; set; }
        public string ClientCountryNM { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TotalAmountLastYear { get; set; }
    }
}
