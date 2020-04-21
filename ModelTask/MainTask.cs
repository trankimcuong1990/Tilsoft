using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTask
{
    public class MainTask : NotificationBase.ITask
    {
        public string GetDescription()
        {
            return "Sending model missing info notification";
        }

        public void ExecuteTask()
        {
            Console.WriteLine("processing");
            Console.ReadLine();
        }
    }
}
