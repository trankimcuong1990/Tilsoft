using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductBreakDownMng.DTO
{
    public class EditFormData
    {
        public DTO.ProductBreakDownDefaultCategoryData DefaultData { get; set; }

        public DTO.ProductBreakDownData MainData { get; set; }

        public List<DTO.ProductBreakDownCalculationTypeData> CalculationType { get; set; }

        public List<DTO.OptionToGetQuantityData> OptionalQuantity { get; set; }

        public List<DTO.OptionToGetPriceData> OptionalPrice { get; set; }

        public EditFormData()
        {
            DefaultData = new ProductBreakDownDefaultCategoryData();
            MainData = new ProductBreakDownData();

            CalculationType = new List<ProductBreakDownCalculationTypeData>();
            OptionalQuantity = new List<OptionToGetQuantityData>();
            OptionalPrice = new List<OptionToGetPriceData>();
        }
    }
}
