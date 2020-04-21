using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityDocumentMng.DTO
{
    public class SupportQualityDocument
    {
        public int ConstantEntryID { get; set; }
        public int? QualityDocumentTypeID { get; set; }
        public string QualityDocumentTypeNM { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
