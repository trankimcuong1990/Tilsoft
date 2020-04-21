using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public class FileUploadViewModel2
    {
        public string ControlID { get; set; }
        public int? TypeID { get; set; }
        public string URLBinding { get; set; }
        public string JSOnChangeHandler { get; set; }
        public string JSONRemoveHandler { get; set; }
        public string FileName { get; set; }
        public bool? ReadOnly { get; set; }
    }
}