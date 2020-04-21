using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class StatusMaterialPrice
    {
        public int ConstantEntryID { get; set; }
        public int StatusID { get; set; }
        public string StatusNM { get; set; }
        public int DisplayOrder { get; set; }
        public string EntryGroup { get; set; }
    }
}
