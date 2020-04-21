using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.WareHouseMng
{
    public class DataContainer
    {
        public DTO.WareHouseMng.WareHouse WareHouseData { get; set; }
        public List<DTO.Support.Country> Countries { get; set; }
    }
}
