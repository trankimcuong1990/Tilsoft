using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ForwarderMng
{
    public class ForwarderSearchResult
    {
        public int ForwarderID { get; set; }
        public string ForwarderUD { get; set; }
        public string ForwarderNM { get; set; }

        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

       
    }
}
