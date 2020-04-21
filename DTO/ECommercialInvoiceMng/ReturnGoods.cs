using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ECommercialInvoiceMng
{
    public class ReturnGoods
    {
        public List<ReturnProduct> ReturnProducts { get; set; }
        public List<ReturnSparepart> ReturnSpareparts { get; set; }
    }
}
