using DTO.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentTermMng.DTO
{
    public class SearchFilterData
    {
        public List<YesNo> YesNoValues { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
    }
}
