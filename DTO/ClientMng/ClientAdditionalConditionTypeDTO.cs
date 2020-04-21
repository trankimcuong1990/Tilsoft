using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientAdditionalConditionTypeDTO
    {
        public long KeyID { get; set; }
        public int ClientAdditionalConditionTypeID { get; set; }
        public string ClientAdditionalConditionTypeNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}
