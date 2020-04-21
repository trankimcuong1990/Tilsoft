using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AppointmentMng.DAL
{
    public partial class AppointmentMngEntities
    {
        public AppointmentMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}
