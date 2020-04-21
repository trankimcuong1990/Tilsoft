using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterExchangeRateMng.DTO
{
    public class MasterExchangeRateSearch
    {
        public int MasterExchangeRateID { get; set; }
        public string ValidDate { get; set; }
        public string Currency { get; set; }
        public string ExchangeRate { get; set; }
    }
}
