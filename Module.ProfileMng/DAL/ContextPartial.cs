using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProfileMng.DAL
{
    public partial class ProfileMngEntities
    {
        public ProfileMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}
