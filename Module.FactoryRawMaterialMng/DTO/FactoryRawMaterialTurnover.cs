using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class FactoryRawMaterialTurnover
    {
        public long FactoryRawMaterialTurnoverID { get; set; }
        public int? FactoryRawMaterialID { get; set; }
        public string Season { get; set; }
        public decimal? Amount { get; set; }
    }
}
