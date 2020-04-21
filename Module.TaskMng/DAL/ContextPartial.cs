using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.TaskMng.DAL
{
    public partial class TaskMngEntities
    {
        public TaskMngEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}
