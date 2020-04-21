using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class InitFormData
    {
        public List<SupportProductBreakDownCalculationTypePALData> SupportProductBreakDownCalculationTypePAL { get; set; }
        public List<SupportProductBreakDownOptionPricePALData> SupportProductBreakDownOptionPricePAL { get; set; }
        public List<SupportProductBreakDownOptionQuantityPALData> SupportProductBreakDownOptionQuantityPAL { get; set; }

        public InitFormData(int getTypeID)
        {
            if (getTypeID == 1)
            {
                SupportProductBreakDownCalculationTypePAL = new List<SupportProductBreakDownCalculationTypePALData>();
                SupportProductBreakDownOptionPricePAL = new List<SupportProductBreakDownOptionPricePALData>();
                SupportProductBreakDownOptionQuantityPAL = new List<SupportProductBreakDownOptionQuantityPALData>();
            }
        }
    }
}
