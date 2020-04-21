using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CatalogFileMng.DTO
{
    public class CatalogFileSearchResult
    {
        public int CatalogFileID { get; set; }
        public string Season { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public string PLFriendlyName { get; set; }
        public string PLFileLocation { get; set; }
    }
}
