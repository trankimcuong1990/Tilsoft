using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2Mng.DTO
{
    public class SearchFilterData
    {
        public List<Support.DTO.Season> Seasons { get; set; }
        public List<Support.DTO.Supplier> Suppliers { get; set; }
    }
}
