using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sale.DTO
{
    public class SaleDTO
    {
        public int SaleID { get; set; }
        public string SaleUD { get; set; }
        public string SaleNM { get; set; }
        public bool? Visible { get; set; }
        public bool? IncludedInDDC { get; set; }
        public bool? IsAccountManager { get; set; }
        public int? CompanyID { get; set; }
        public int? UserID { get; set; }
    }
}
