using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DTO
{
    public class SearchDTO
    {
        public SearchDTO()
        {
            datas = new List<AgentsDTO> { };
        }
        public List<AgentsDTO> datas { get; set; }
    }
}
