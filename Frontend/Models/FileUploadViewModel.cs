using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public class FileUploadViewModel
    {
        public string ControlID { get; set; }
        public string URLBinding { get; set; }
        public string JSOnChangeHandler { get; set; }
        public string JSONRemoveHandler { get; set; }
        public string FileName {get;set;}
    }
}