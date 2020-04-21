using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ShowroomItemMng
{
    public class ShowroomItem
    {
        public int? ShowroomItemID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public int? MaterialID { get; set; }

        public int? MaterialTypeID { get; set; }

        public int? MaterialColorID { get; set; }

        public int? FrameMaterialID { get; set; }

        public int? FrameMaterialColorID { get; set; }

        public int? SubMaterialID { get; set; }

        public int? SubMaterialColorID { get; set; }

        public int? SeatCushionID { get; set; }

        public int? BackCushionID { get; set; }

        public int? CushionColorID { get; set; }

        public int? ProductID { get; set; }

        public int? SampleProductID { get; set; }

        public int? CreatedBy { get; set; }
        
        public string CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public string ProductArticleCode { get; set; }

        public string ProductDescription { get; set; }

        public string ImageFile { get; set; }
        public string ShowroomItemThumbnailImage { get; set; }
        public string ShowroomItemFullImage { get; set; }
        public bool? ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }
        public string SampleArticleDescription { get; set; }

        public string CreatorName2 { get; set; }
        public string UpdatorName2 { get; set; }
    }
}