using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionStatisticsMng.DTO
{
    public class EditFormData
    {
        public EditFormData()
        {
            Data = new ProductionStatisticsDTO();
        }
        public ProductionStatisticsDTO Data { get; set; }
    }
}
