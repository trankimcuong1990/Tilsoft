using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.EmployeeMng.DAL
{
    public partial class EmployeeMngEntities
    {
        public EmployeeMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}
