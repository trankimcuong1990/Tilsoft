using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ComplianceMng.DAL
{   

    public partial class ComplianceMngEntities
    {
        public ComplianceMngEntities(string iConnectionString)
           : base(iConnectionString)
        { }
    }
}
