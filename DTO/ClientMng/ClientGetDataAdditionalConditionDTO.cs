using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientMng
{
    public class ClientGetDataAdditionalConditionDTO
    {        
        public int AdditionalConditionID { get; set; }
        public string AdditionalConditionTypeNM { get; set; }
        public string AdditionalConditionNM { get; set; }
        public string Remark { get; set; }
        public bool? ActiveFactory { get; set; }
        public bool? WhoToPay { get; set; }       
    }
}
