using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ClientContactMng
{
    public class ClientContactSearchResult
    {
        public int ClientContactID { get; set; }
        public string ClientNM { get; set; }
        public string Salutation { get; set; }
        public string FullName { get; set; }
        public string CallingName { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string UpdatorName { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
