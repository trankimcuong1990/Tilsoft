using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductTestingMng.DTO
{
    public class ProductTestStandardSearchViewDTO
    {
        public int ProductTestUsingTestStandardID { get; set; }
        public int? ProductTestID { get; set; }
        public string TestStandardNM { get; set; }

    }
}
