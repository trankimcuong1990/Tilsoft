using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryProductionStatus.DTO
{
    public class FactoryProductionStatusSearchDTO
    {
        public int? FactoryProductionStatusID { get; set; }
        public int? FactoryID { get; set; }
        public string Season { get; set; }
        public int? WeekNo { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public decimal? FactoryCapacity { get; set; }
        public string StatusDate { get; set; }
        public string FactoryUD { get; set; }
    }
}
