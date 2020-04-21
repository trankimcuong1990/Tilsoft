using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AdditionalConditionMng.DTO
{
    public class AdditionalConditionTypeDTO
    {
        public long KeyID { get; set; }
        public int AdditionalConditionTypeID { get; set; }
        public string AdditionalConditionTypeNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}
