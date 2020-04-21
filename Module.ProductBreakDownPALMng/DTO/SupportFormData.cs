using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownPALMng.DTO
{
    public class SupportFormData
    {
        public List<SupportProductBreakDownPALSampleProductData> SupportSampleProduct { get; set; }
        public List<SupportProductBreakDownPALProductData> SupportProduct { get; set; }

        public SupportFormData()
        {
            SupportSampleProduct = new List<SupportProductBreakDownPALSampleProductData>();
            SupportProduct = new List<SupportProductBreakDownPALProductData>();
        }
    }
}
