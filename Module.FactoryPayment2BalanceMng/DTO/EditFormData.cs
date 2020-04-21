using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryPayment2BalanceMng.DTO
{
    public class EditFormData
    {
        public DTO.FactoryPayment2BalanceSearchResult Data { get; set; }
        public List<DTO.Detail> Details { get; set; }
    }
}
