using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class CapacityData
    {
        public List<CapacityOverview> CapacityOverviews { get; set; }

        public CapacityData()
        {
            CapacityOverviews = new List<CapacityOverview>();
        }
    }
}
