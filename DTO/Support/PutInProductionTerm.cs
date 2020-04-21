using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class PutInProductionTerm
    {
        public int ConstantEntryID { get; set; }
        public int PutInProductionTermID { get; set; }
        public string PutInProductionTermNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}
