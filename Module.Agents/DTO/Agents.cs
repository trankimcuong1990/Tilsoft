using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Agents.DTO
{
    public class AgentsDTO
    {
        public int AgentID { get; set; }
        public string AgentUD { get; set; }
        public string AgentNM { get; set; }
        public bool? Visible { get; set; }
    }
}
