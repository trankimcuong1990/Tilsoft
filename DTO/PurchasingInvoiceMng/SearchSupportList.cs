using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PurchasingInvoiceMng
{
    public class SearchSupportList
    {
        public List<Support.Season> Seasons { get; set; }
        public List<Support.Supplier> Suppliers { get; set; }
    }
}
