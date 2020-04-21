using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ClientSpecificationMng.DTO
{
    public class FactorySpecificationDTO
    {
        public int FactorySpecificationID { get; set; }
        public int? ClientSpecificationID { get; set; }
        public string FactoryUD { get; set; }
        public string FactorySpecificationTypeNM { get; set; }
        public string FriendlyName { get; set; }
        public string FileLocation { get; set; }
        public int CommentNo { get; set; }
        public int? FactorySpecificationUpdatedBy { get; set; }
        public string FactorySpecificationUpdatedDate { get; set; }
        public string UpdatorName { get; set; }
        public bool? HasUrlLink { get; set; }
        public string FactorySpecificationRemark { get; set; }

        public List<FactorySpecificationCommentDTO> FactorySpecificationComments { get; set; }
    }
}
