using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientGroupMng
{
    public class ClientGroupSearchResult
    {
        public int ClientGroupID { get; set; }
        public string ClientGroupNM { get; set; }
        public string Remark { get; set; }
        public string CreatorName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
