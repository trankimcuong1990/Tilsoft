using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentTermMng.DTO
{
    public class PaymentTerm
    {
        public int? PaymentTermID { get; set; }
        public string PaymentTermUD { get; set; }
        public string PaymentTermNM { get; set; }
        public int? PaymentTypeID { get; set; }
        public decimal? DownValue { get; set; }
        public bool? IsLCCost { get; set; }
        public bool? IsActive { get; set; }
        public string ConcurrencyFlag_String { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string PaymentMethod { get; set; }
        public int? InDays { get; set; }
    }
}
