using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountReceivableRpt.DTO
{
    public class ListColorCatagoryDTO
    {
        public int ColorID { get; set; }
        public string ColorUD { get; set; }
        public string ColorName { get; set; }
        public decimal? TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }
}
