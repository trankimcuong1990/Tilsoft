using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonMng.DTO
{
    public class AccManagerDTO
    {
        public int EmployeeID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string SaleUD { get; set; }
        public string EmployeeNM { get; set; }
        public string EmployeeFirstNM { get; set; }
    }
}
