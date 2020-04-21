using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.Magento2.DTO.POST
{
    public class Customer
    {
        public CustomerDetail customer { get; set; }
        public string password { get; set; }
    }
}
