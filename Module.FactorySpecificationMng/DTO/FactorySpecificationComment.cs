using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactorySpecificationMng.DTO
{
    public class FactorySpecificationComment
    {
        public int? FactorySpecificationCommentID { get; set; }
        public int? FactorySpecificationID { get; set; }
        public string Comment { get; set; }
        public string UpdatorNM { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string FileLocation { get; set; }
        public bool CanDelete { get; set; }
    }
}
