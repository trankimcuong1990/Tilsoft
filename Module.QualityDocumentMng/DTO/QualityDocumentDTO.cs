using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityDocumentMng.DTO
{
    public class QualityDocumentDTO
    {
        public int QualityDocumentID { get; set; }
        public string QualityDocumentUD { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? QualityDocumentTypeID { get; set; }
        public string IssuedDate { get; set; }
        public string AttachedFile { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }

        public bool? File_HasChange { get; set; }
        public string File_NewFile { get; set; } 
    }
}
