using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySpecificationMng.DTO
{
    public class FactorySpecificationOfferLinePI
    {
        public int PrimaryID { get; set; }
        public int FactorySpecificationID { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ArticleCode { get; set; }
    }
}
