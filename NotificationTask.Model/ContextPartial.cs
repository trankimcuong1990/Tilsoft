using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.Model
{
    public partial class ModelNotificationEntities
    {
        public ModelNotificationEntities(string iConnectionString)
            : base(iConnectionString)
        { }
    }
}
