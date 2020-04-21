using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.WorkOrder.DTO
{
    public class SampleOrderDTO
    {
        public int SampleOrderID { get; set; }
        public string SampleOrderInfo { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public List<SampleProductDTO> SampleProductDTOs { get; set; }
    }

    public class SampleProductDTO
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string ArticleDescription { get; set; }
        public string ETADestination { get; set; }
    }
}
