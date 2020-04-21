using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountReceivableRpt.DTO
{
    public class ListChartDetailDTO
    {
        public int? DueColorID { get; set; }
        public string DueColorUD { get; set; }
        public string DueColorNM { get; set; }
        public string DueColorDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int TotalCount { get; set; }
        public int? ToDue { get; set; }
    }
}
