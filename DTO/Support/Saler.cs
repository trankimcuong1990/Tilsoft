using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class Saler
    {
        public int SaleID { get; set; }
        public string SaleUD { get; set; }
        public string saleNM { get; set; }
        public int? CompanyID { get; set; }
        public int UserID { get; set; }
        public bool? IsAccountManager { get; set; }
    }
}
