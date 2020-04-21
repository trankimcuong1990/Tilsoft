using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationBase
{
    public interface ITask
    {
        string GetDescription();
        void ExecuteTask();
    }
}
