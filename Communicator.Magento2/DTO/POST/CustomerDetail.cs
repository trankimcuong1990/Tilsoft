using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.Magento2.DTO.POST
{
    public class CustomerDetail
    {
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        //public List<CustomerAddress> addresses { get; set; }
    }
}
