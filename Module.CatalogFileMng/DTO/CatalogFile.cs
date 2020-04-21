using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CatalogFileMng.DTO
{
    public class CatalogFileDTO
    {
        public int CatalogFileID { get; set; }
        public string Season { get; set; }
        public string FileUD { get; set; }
        public string Comment { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string EmployeeNM { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }

        public string NewFile { get; set; }
        public bool HasChanged { get; set; }

        public string PriceListFileUD { get; set; }
        public string PLFriendlyName { get; set; }
        public string PLFileLocation { get; set; }
        public string PLNewFile { get; set; }
        public bool PLHasChanged { get; set; }
    }
}
