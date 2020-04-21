using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AccountMng
{
    public partial class AccountMngEntities
    {
        public AccountMngEntities(string iConnectionString)
            : base(iConnectionString)
        {
        }
    }
}
