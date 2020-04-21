using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionPriceMng.DTO
{
    public class EditFormData
    {
        public EditFormData()
        {
            Data = new ProductionPriceDTO();
        }
        public ProductionPriceDTO Data { get; set; }        
    }
}
