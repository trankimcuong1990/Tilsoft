using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class ConfirmedProduct
    {
        public int ProductID { get; set; }
        
        public string ArticleCode { get; set; }
        
        public string Description { get; set; }

        public string ImageFileFullSize { get; set; }

        public string ImageFile { get; set; }
    }
}