using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PaymentNoteMng.DTO
{
    public class SearchPO
    {
        public SearchPO()
        {
            Data = new List<PaymentNoteSupportSearchPO>();
        }
        public int totalRows { get; set; }
        public List<DTO.PaymentNoteSupportSearchPO> Data { get; set; }
    }
}
