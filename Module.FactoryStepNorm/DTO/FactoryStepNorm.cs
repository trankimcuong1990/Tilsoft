using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryStepNorm.DTO
{
    public class FactoryStepNorm
    {
        public int? FactoryStepNormID { get; set; }
        public int? ProductID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public List<FactoryStepNormDetail> FactoryStepNormDetails { get; set; }
    }
}
