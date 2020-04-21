using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.QuotationMng
{
    public class InitFormData
    {
        public List<Support.Factory> Factories { get; set; }
        public List<FactoryOrderSearchResult> Orders { get; set; }
        public List<Support.Season> Seasons { get; set; }
    }
}
