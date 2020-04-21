using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class BOMStandardImportDTO
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Sequence { get; set; }
        public bool? IsEndOfSequence { get; set; }
        public int? DefaultFactoryWarehouseID { get; set; }
    }
}
