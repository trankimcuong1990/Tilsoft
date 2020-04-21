using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryNorm.DTO
{
    public class FactoryNorm
    {
        public int FactoryNormID { get; set; }
        public int? ModelID { get; set; }
        public string ModelUD { get; set; }
        public string ModelNM { get; set; }
        public List<FactoryFinishedProductNorm> FactoryFinishedProductNorms { get; set; }
    }
}
