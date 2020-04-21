using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.UserFileMng.DTO
{
    public class FileSearchResult
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string AbsoluteURL { get; set; }
    }
}
