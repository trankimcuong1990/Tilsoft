using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOMStandardMng.DTO
{
    public class ViewFormData
    {
        public BOMStandardViewDTO Data { get; set; }
        public List<Module.Support.DTO.ProductionItemType> ProductionItemTypes { get; set; }
        public ViewFormData()
        {
            Data = new BOMStandardViewDTO();
            ProductionItemTypes = new List<Support.DTO.ProductionItemType>();
        }
    }
}
