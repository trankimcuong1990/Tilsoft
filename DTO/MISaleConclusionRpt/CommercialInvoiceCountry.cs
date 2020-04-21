using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.MISaleConclusionRpt
{
    public class CommercialInvoiceCountry
    {
        public int? ClientCountryID { get; set; }
        public string ClientCountryUD { get; set; }
        public string ClientCountryNM { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalCont { get; set; }
        public int? TotalClient { get; set; }
        public decimal? TotalAmountLastYear { get; set; }
    }
}
