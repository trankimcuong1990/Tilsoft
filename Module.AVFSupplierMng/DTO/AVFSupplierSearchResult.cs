using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AVFSupplierMng.DTO
{
    public class AVFSupplierSearchResult
    {
        public int AVFSupplierID { get; set; }
        public string AVFSupplierNM { get; set; }
        public decimal? OpeningDebit { get; set; }
        public decimal? OpeningCredit { get; set; }
        public decimal? IncreasingDebit { get; set; }
        public decimal? IncreasingCredit { get; set; }
        public decimal? ClosingDebit { get; set; }
        public decimal? ClosingCredit { get; set; }
    }
}
