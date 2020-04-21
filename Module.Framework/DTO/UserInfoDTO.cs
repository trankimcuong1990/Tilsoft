using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Framework.DTO
{
    public class UserInfoDTO
    {
        public int UserID { get; set; }
        public int UserCompanyID { get; set; }
        public int UserBranchID { get; set; }
        public int UserFactoryID { get; set; }
        public int? UserCientID { get; set; }
    }
}
