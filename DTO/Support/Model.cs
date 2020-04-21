using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DTO.Support
{
    public class Model
    {
        public int ModelID { get; set; }
        public string ModelUD { get; set; }
        public string EANCode { get; set; }
        public string ModelNM { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductTypeNM { get; set; }
        public bool IsSelected { get; set; }
        public string OverallDimL { get; set; }
        public string OverallDimW { get; set; }
        public string OverallDimH { get; set; }
        public string FileLocation { get; set; }
    }
}
