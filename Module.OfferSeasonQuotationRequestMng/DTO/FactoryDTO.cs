using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OfferSeasonQuotationRequestMng.DTO
{
    public class FactoryDTO
    {
        public int FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public bool IsSelected { get; set; }
        public bool IsHidden { get; set; }
        public int IsPrefered { get; set; }
    }
}
