using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class FactoryFinishedProducts
    {
        public List<FactoryFinishedProduct> Data { get; set; }
        public int TotalRows { get; set; }
    }

    public class FactoryFinishedProduct
    {
        public int FactoryFinishedProductID { get; set; }
        public string FactoryFinishedProductUD { get; set; }
        public string FactoryFinishedProductNM { get; set; }
    }
}
