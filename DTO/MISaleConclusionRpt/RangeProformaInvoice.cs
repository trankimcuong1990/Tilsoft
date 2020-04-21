using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleConclusionRpt
{
    public class RangeProformaInvoice
    {
        public string RangeNM { get; set; }
        public int? TotalClient { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
