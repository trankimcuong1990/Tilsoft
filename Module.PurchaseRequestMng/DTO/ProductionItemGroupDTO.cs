using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PurchaseRequestMng.DTO
{
    public class ProductionItemGroupDTO
    {
        public int ProductionItemGroupID { get; set; }
        public string ProductionItemGroupNM { get; set; }
        public bool? IsSelected { get; set; }
    }
}
