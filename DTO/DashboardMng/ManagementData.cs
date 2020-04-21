using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DashboardMng
{
    public class ManagementData
    {
        public List<DDC> DDCs { get; set; }
        public List<Production> Productions { get; set; }
        public List<Proforma> Proformas { get; set; }
    }
}
