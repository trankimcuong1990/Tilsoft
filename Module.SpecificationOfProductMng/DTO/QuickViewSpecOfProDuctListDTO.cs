using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng.DTO
{
    public class QuickViewSpecOfProDuctListDTO
    {
        public int ProductSpecificationID { get; set; }
        public string ProductUD { get; set; }
        public string ArticleDescription { get; set; }
        public string ClientNM { get; set; }
        public string ClientUD { get; set; }
        public string Remark { get; set; }
        public int? ModelID { get; set; }
    }
}
