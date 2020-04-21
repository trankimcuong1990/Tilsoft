using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ModelMng
{
    public class ModelDefaultFactoryDetail
    {
        public int PrimaryID { get; set; }
        public decimal? SalePrice { get; set; }
        public int? OrderQnt { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ModelID { get; set; }
        public int? FactoryID { get; set; }
        public string ClientUD { get; set; }
    }
}
