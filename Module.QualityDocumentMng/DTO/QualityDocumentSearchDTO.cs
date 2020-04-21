using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityDocumentMng.DTO
{
    public class QualityDocumentSearchDTO
    {
        public int QualityDocumentID { get; set; }
        public string QualityDocumentUD { get; set; }
        public string Description { get; set; }
        public string IssuedDate { get; set; }
        public string ClientNM { get; set; }
        public string UpdatedDate { get; set; }
        public string QualityDocumentTypeNM { get; set; }

        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
    }
}
