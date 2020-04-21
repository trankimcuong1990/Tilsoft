using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Support.DTO
{
    public class SampleProduct
    {
        public int SampleProductID { get; set; }
        public string ArticleDescription { get; set; }
        public int? ClientID { get; set; }
        public string ClientUD { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
    }
}
