using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStockReceipt.DTO
{
    public class EditFormData
    {
        public FactoryStockReceipt Data { get; set; }
        public List<Support.DTO.Factory> Factories { get; set; }
    }
}
