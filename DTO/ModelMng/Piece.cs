using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMng
{
    public class Piece
    {
        public int ModelPieceID { get; set; }

        [Display(Name = "PieceModelID")]
        public int? PieceModelID { get; set; }
        
        [Display(Name = "ModelUD")]
        public string ModelUD { get; set; }

        [Display(Name = "EANCode")]
        public string EANCode { get; set; }

        [Display(Name = "ModelNM")]
        public string ModelNM { get; set; }

        [Display(Name = "ProductTypeNM")]
        public string ProductTypeNM { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "RowIndex")]
        public int? RowIndex { get; set; }

        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
    }
}