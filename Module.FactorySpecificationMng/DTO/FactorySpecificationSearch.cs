using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySpecificationMng.DTO
{
    public class FactorySpecificationSearch
    {
        public int? FactorySpecificationID { get; set; }
        public string Code { get; set; }
        public string ClientUD { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ClientSpecificationTypeID { get; set; }
        public string ClientFileFriendlyName { get; set; }
        public string ClientFileLocation { get; set; }
        public string FactoryFileFriendlyName { get; set; }
        public string FactoryFileLocation { get; set; }
        public string FactorySpecificationUpdatedDate { get; set; }
        public string UpdatorNM { get; set; }
        public int? FactorySpecificationTypeID { get; set; }
        public int? FactorySpecificationUpdatedBy { get; set; }

        public List<FactorySpecificationOfferLinePI> FactorySpecificationPIs { get; set; }
    }
}
