using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientRelationshipTypeMng.DTO
{
   public class ClientRelationshipTypeDto
    {
        public int ClientRelationshipTypeID { get; set; }
        public string ClientRelationshipTypeUD { get; set; }
        public string ClientRelationshipTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? isChange { get; set; }
    }
}
