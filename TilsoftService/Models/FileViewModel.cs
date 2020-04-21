using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TilsoftService.Models
{
    public class FileViewModel
    {
        public string FileName { get; set; }
        public string FriendlyName { get; set; }
        public string FileExtension { get; set; }
        public string URL { get; set; }        
    }
}