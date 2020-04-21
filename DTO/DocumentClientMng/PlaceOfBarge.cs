using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace DTO.DocumentClientMng
{
    public class PlaceOfBarge
    {
        [Display(Name = "Place Of Barge")]
        public int PlaceOfBargeID { get; set; }
        [Display(Name = "Place Of Barge Code")]
        public string PlaceOfBargeUD { get; set; }
        [Display(Name = "Place Of Barge Name")]
        public string PlaceOfBargeNM { get; set; }
    }
}
