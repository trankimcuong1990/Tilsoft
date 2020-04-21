using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MasterExchangeRateMng.DTO
{
    public class UpdateIndexForm
    {
        public int MasterExchangeRateID { get; set; }
        public string ValidDate { get; set; }
        public string Currency { get; set; }
        public decimal? ExchangeRate { get; set; }
    }
}
