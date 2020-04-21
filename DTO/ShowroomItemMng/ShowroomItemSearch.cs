using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ShowroomItemMng
{
    public class ShowroomItemSearch
    {
        public int? ShowroomItemID { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public string MaterialNM { get; set; }

        public string MaterialTypeNM { get; set; }

        public string MaterialColorNM { get; set; }

        public string FrameMaterialNM { get; set; }

        public string FrameMaterialColorNM { get; set; }

        public string SubMaterialNM { get; set; }

        public string SubMaterialColorNM { get; set; }

        public string SeatCushionNM { get; set; }

        public string BackCushionNM { get; set; }

        public string CushionColorNM { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

    }
}