using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CheckListMng.DTO
{
    public class TypeOfInspectionDTO
    {
        public long KeyID { get; set; }
        public Nullable<int> TypeOfInspecID { get; set; }
        public string TypeOfInspecNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    }
}
