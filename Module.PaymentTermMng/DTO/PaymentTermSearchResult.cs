using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentTermMng.DTO
{
    public class PaymentTermSearchResult
    {
        public int? PaymentTermID { get; set; }
        public string PaymentTermUD { get; set; }
        public string PaymentTermNM { get; set; }
        public string PaymentTypeNM { get; set; }
        public decimal? DownValue { get; set; }
        public string PercentDown { get; set; }
        public string MoneyDown { get; set; }
        public bool? IsLCCost { get; set; }
        public bool? IsActive { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string PaymentMethod { get; set; }
        public int? InDays { get; set; }
    }
}
