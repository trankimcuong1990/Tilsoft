using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BOM.DTO.WorkOrderAndProductionProcessForm
{
    public class ProductionProcessFormData
    {
        public List<ProductionProcessDTO> ProductionProcesses { get; set; }
    }

    public class ProductionProcessDTO
    {
        public int ProductionProcessID { get; set; }
        public int? ModelID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductArticleCode { get; set; }
        public int? BOMStandardID { get; set; }
        public string ModelUD { get; set; }
        public bool? IsDefaultOfModel { get; set; }
        public string FrameMaterialNM { get; set; }

    }
}
