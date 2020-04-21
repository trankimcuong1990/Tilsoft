using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientSpecificationMng.DTO
{
    public class FactorySpecificationCommentDTO
    {
        public int FactorySpecificationCommentID { get; set; }
        public int? FactorySpecificationID { get; set; }
        public string Comment { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasUrlLink { get; set; }
        public int PrimaryID { get; set; }
        public bool? CanDelete { get; set; }
        public string ThumbnailLocation { get; set; }
    }
}
