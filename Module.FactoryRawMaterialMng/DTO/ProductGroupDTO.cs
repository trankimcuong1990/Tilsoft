using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryRawMaterialMng.DTO
{
    public class ProductGroupDTO
    {
        public int ConstantEntryID { get; set; }
        public Nullable<int> ProductGroupID { get; set; }
        public string ProductGroupNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsChecked { get; set; }
    }
}
