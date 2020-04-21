using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryContactQuickInfo
    {
        public int FactoryContactQuickInfoID { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
    }
}
