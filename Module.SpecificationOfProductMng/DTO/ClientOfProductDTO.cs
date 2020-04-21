using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng.DTO
{
    public class ClientOfProductDTO
    {
        public long KeyID { get; set; }
        public int ProductID { get; set; }
        public string ClientUD { get; set; }
        public string ClientNM { get; set; }
        public int? ClientID { get; set; }
    }
}
