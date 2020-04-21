using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BackCushionMng.DTO
{
    public class ProductGroupDTO
    {
        public int BackCushionProductGroupID { get; set; }
        public int ProductGroupID { get; set; }
        public bool IsEnabled { get; set; }
        public string ProductGroupNM { get; set; }
    }
}
