using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportFreeToSale
{
    public class SearchFormData
    {
        public List<FreeToSale> Data { get; set; }

        public int? TotalFTSQnt { get; set; }

        public decimal? TotalFTSQntIn40HC { get; set; }
    }
}
    