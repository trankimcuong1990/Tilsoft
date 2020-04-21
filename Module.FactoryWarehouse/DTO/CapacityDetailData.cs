using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryWarehouse.DTO
{
    public class CapacityDetailData
    {
        public List<PalletOverview> PalletOverviews { get; set; }

        public CapacityDetailData()
        {
            PalletOverviews = new List<PalletOverview>();
        }
    }
}
