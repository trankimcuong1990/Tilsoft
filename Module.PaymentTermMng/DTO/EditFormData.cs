using DTO.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentTermMng.DTO
{
    public class EditFormData
    {
        public DTO.PaymentTerm Data { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<YesNo> ActiveStatuses { get; set; }
    }
}
