using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ProductMng
{
    public class Package
    {
        public int PackageID { get; set; }
        public string PackageNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
