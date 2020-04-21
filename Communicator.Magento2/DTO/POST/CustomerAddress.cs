using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.Magento2.DTO.POST
{
    public class CustomerAddress
    {
        public bool defaultShipping { get; set; }
        public bool defaultBilling { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public CustomerAddressRegion region { get; set; }
        public string postcode { get; set; }
        public string telephone { get; set; }
        public List<string> street { get; set; }
        public string city { get; set; }
        public string countryId { get; set; }
        public string company { get; set; }
    }
}
