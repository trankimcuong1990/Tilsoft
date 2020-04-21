using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityDocumentMng.DTO
{
    public class EditForm
    {
        public DTO.QualityDocumentDTO Data { get; set; }
        public List<DTO.SupportQualityDocument> supportQualityDocuments { get; set; }
    }
}
