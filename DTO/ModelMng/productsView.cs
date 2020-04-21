using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class productsView
    {
        public int ProductID { get; set; }
        public int? ModelID { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
    }
}
