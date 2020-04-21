using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Support
{
    public class PackagingMethod
    {
        public int PackagingMethodID { get; set; }
        public string PackagingMethodUD { get; set; }
        public string PackagingMethodNM { get; set; }
        public bool IsSelected { get; set; }
    }
}
