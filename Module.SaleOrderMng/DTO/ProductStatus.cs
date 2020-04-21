using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SaleOrderMng.DTO
{
    public class ProductStatus
    {
        public int ConstantEntryID { get; set; }
        public int ProductStatusID { get; set; }
        public string ProductStatusNM { get; set; }
        public int DisplayOrder { get; set; }
    }
}
