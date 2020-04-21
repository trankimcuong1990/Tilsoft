using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientSpecificationMng.DTO
{
    public class ClientSpecificationStandardFileDTO
    {
        public int PrimaryID { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string FileUD { get; set; }
    }
}
