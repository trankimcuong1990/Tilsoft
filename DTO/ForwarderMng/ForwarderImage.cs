using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ForwarderMng
{
    public class ForwarderImage
    {
        public int ForwarderImageID { get; set; }

        public int? ForwarderID { get; set; }

        public string FileUD { get; set; }

        public string Remark { get; set; }

        public string FriendlyName { get; set; }

        public string FileLocation { get; set; }

        public string ThumbnailLocation { get; set; }

        public bool? File_HasChange { get; set; }

        public string File_NewFile { get; set; }

        public string ContactPersonNM { get; set; }
        public string JobTitle { get; set; }
        public int? InternalDepartmentID { get; set; }
        public string InternalDepartmentNM { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
    }
}
