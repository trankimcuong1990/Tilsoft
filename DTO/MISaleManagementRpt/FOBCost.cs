using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleManagementRpt
{
    public class FOBCost
    {
        public string Season { get; set; }
        public int? SaleID { get; set; }
        public decimal? TotalCostAmountEUR { get; set; }
    }
}
