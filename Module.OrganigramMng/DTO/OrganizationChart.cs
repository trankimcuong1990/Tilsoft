using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.OrganigramMng.DTO
{
    public class OrganizationChart
    {
        public int? OrganigramID { get; set; }
        public int? CompanyID { get; set; }
        public string CompanyUD { get; set; }
        public string CompanyNM { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string ImageFile { get; set; }
        public string WorkSpaceFile { get; set; }
        public int? LocationID { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string FriendlyName { get; set; }
        public bool ImageFile_HasChange { get; set; }
        public string ImageFile_NewFile { get; set; }

        public bool WorkSpaceFile_HasChanged { get; set; }
        public string WorkSpaceFile_NewFile { get; set; }
        public string WorkSpaceFileThumbnail { get; set; }
        public string WorkSpaceFileLocation { get; set; }
    }
}
