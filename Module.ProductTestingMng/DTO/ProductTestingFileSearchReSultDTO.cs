using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductTestingMng.DTO
{
    public class ProductTestingFileSearchReSultDTO
    {
        public int ProductTestFileID { get; set; }
        public int? ProductTestID { get; set; }
        public string Remark { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
    }
}
