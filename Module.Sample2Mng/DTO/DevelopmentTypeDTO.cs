using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Sample2Mng.DTO
{
    public class DevelopmentTypeDTO
    {
        public long KeyID { get; set; }
        public Nullable<int> DevelopmentTypeID { get; set; }
        public string DevelopmentTypeNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
