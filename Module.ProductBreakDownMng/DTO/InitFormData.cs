using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class InitFormData
    {
        public List<DTO.ProductBreakDownCalculationTypeData> CalculationType { get; set; }

        public List<DTO.OptionToGetQuantityData> OptionalQuantity { get; set; }
        public List<Support.DTO.Client> Client { get; set; }
    }
}
