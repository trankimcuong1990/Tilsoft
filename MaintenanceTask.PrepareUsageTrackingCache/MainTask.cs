using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.PrepareUsageTrackingCache
{
    public class MainTask : NotificationBase.ITask
    {
        public void ExecuteTask()
        {
            try
            {
                using (UsageTrackingEntities context = new UsageTrackingEntities(Library.Helper.CreateEntityConnectionString("UsageTrackingModel")))
                {
                    context.UsageTrackingRpt_function_prepareCacheData();
                }
            }
            catch { }            
        }

        public string GetDescription()
        {
            return "Prepare cache for usage tracking information!";
        }
    }
}
