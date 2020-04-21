using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySaleInvoice.DTO
{
    public class FactorySaleOrderSearchFromData
    {
        public FactorySaleOrderSearchFromData()
        {
            Data = new List<FactorySaleOrderItemDTO>();
        }
        public List<FactorySaleOrderItemDTO> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
