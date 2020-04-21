using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class SampleProductView
    {
        public int SampleProductID { get; set; }
        public int? ModelID { get; set; }
        public string SampleProductUD { get; set; }
        public string ArticleDescription { get; set; }
        public int? SampleOrderID { get; set; }
    }
}
