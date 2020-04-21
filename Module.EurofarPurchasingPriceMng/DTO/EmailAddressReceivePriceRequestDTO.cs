using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EurofarPurchasingPriceMng.DTO
{
    public class EmailAddressReceivePriceRequestDTO
    {
        public int FactoryID { get; set; }
        public string EmailAddress { get; set; }
    }
}
