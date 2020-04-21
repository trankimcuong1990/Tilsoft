using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySpecificationMng.DTO
{
    public class FactorySpecification
    {
        public int? FactorySpecificationID { get; set; }
        public string Code { get; set; }
        public string ClientUD { get; set; }
        public int? FactoryID { get; set; }
        public string FactoryUD { get; set; }
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public int? ModelID { get; set; }
        public int? ClientSpecificationTypeID { get; set; }
        public string ClientSpecificationRemark { get; set; }
        public string ClientFileFriendlyName { get; set; }
        public string ClientFileLocation { get; set; }
        public string ClientSpecificationUpdatedDate { get; set; }
        public int ClientSpecificationUpdatedBy { get; set; }
        public string ClientUpdatorNM { get; set; }

        // Factory File (possible to change)
        public string FactorySpecificationFileUD { get; set; }
        public bool FactoryFileLocation_HasChange { get; set; }
        public string NewFile { get; set; }
        public string FactoryFileFriendlyName { get; set; }
        public string FactoryFileLocation { get; set; }

        public string FactorySpecificationRemark { get; set; }
        public int FactorySpecificationTypeID { get; set; }
        public string FactorySpecificationUpdatedDate { get; set; }
        public int FactorySpecificationUpdatedBy { get; set; }
        public string FactoryUpdatorNM { get; set; }
        public string ClientSpecificationTypeNM { get; set; }

        public List<DTO.FactorySpecificationComment> FactorySpecificationComments { get; set; }
    }
}
