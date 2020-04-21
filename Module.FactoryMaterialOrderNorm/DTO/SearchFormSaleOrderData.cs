using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMaterialOrderNorm.DTO
{
    public class SearchFormClientData
    {
        public List<Client> Data { get; set; }
        public int TotalRows { get; set; }
        public List<Module.Support.DTO.Season> Seasons { get; set; }
    }
}
