using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakDownMng.DTO
{
    public class BreakdownPriceHistoryDTO
    {
        public int BreakdownPriceHistoryID { get; set; }
        public string BreakdownPriceHistoryUD { get; set; }
        public string BreakdownPriceHistoryNM { get; set; }
        public string ArticleCode { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> BreakdownID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<BreakdownPriceHistoryDetailDTO> BreakdownPriceHistoryDetailDTOs { get; set; }
    }
}
