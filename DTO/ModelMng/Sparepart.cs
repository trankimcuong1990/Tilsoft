using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.ModelMng
{
    public class Sparepart
    {
        public int ModelSparepartID { get; set; }

        [Display(Name = "PartUD")]
        public string PartUD { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}