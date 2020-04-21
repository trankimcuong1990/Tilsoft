using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryMng2.DTO
{
    public class FactoryDocumentDTO
    {
        public int FactoryDocmentID { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryDocumentFile { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string Remark { get; set; }
        public bool FactoryDocumentHasChange { get; set; }
        public string FactoryDocumentNewFile { get; set; }
    }
}
