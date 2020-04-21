using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.FactoryOrderMng
{
    public class FactoryListByStatus
    {
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string FactoryName { get; set; }
        public int? LocationID { get; set; }
        public bool? IsActive { get; set; }
        public string LocationNM { get; set; }
    }
}
