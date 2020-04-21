using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportStockOverview
{
    public class ProductImportRemark
    {
        public object FullKeyID { get; set; }
        public string KeyID { get; set; }
        public int? GoodsID { get; set; }
        public int? ProductStatusID { get; set; }
        public string ProductType { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }

    }
}
