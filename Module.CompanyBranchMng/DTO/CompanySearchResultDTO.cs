using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CompanyBranchMng.DTO
{
    public class CompanySearchResultDTO
    {
        public int CompanyID { get; set; }
        public string CompanyUD { get; set; }
        public string CompanyNM { get; set; }
        public string Logo { get; set; }
        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public string ShortName { get; set; }
        public string OfficialName { get; set; }
        public string OfficialAddress { get; set; }
        public string StreetAddress { get; set; }
        public int? LocationID { get; set; }
        public string LocationUD { get; set; }
        public string LocationNM { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Remark { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatorName { get; set; }
        public string UpdatedDate { get; set; }
    }
}
