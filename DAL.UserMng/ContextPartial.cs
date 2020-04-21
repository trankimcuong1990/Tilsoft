using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserMng
{
    public partial class UserMngEntities
    {
        public UserMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
