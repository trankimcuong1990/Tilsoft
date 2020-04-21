using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceTask.PreparePricingCache
{
    public class MainTask : NotificationBase.ITask
    {
        public void ExecuteTask()
        {
            try
            {
                string currentSeason = Library.OMSHelper.Helper.GetCurrentSeason();
                string prevSeason = Library.OMSHelper.Helper.GetPrevSeason(currentSeason);
                string nextSeason = Library.OMSHelper.Helper.GetNextSeason(currentSeason);

                using (CacheMngEntities context = new CacheMngEntities(Library.Helper.CreateEntityConnectionString("CacheMngModel")))
                {
                    context.FW_function_GenerateEurofarPurchasingPriceCachedData(Library.OMSHelper.Helper.GetCurrentSeason());
                    context.FW_function_GenerateEurofarPurchasingPriceCachedData(Library.OMSHelper.Helper.GetPrevSeason(currentSeason));
                    context.FW_function_GenerateEurofarPurchasingPriceCachedData(Library.OMSHelper.Helper.GetNextSeason(currentSeason));
                }
            }
            catch { }
        }

        public string GetDescription()
        {
            string currentSeason = Library.OMSHelper.Helper.GetCurrentSeason();
            string prevSeason = Library.OMSHelper.Helper.GetPrevSeason(currentSeason);
            string nextSeason = Library.OMSHelper.Helper.GetNextSeason(currentSeason);

            return "Prepare cache for pricing module, season: ["+ currentSeason + "], [" + prevSeason + "], [" + nextSeason + "]";
        }
    }
}
