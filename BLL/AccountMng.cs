using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class AccountMng
    {
        private DAL.AccountMng.DataFactory factory = new DAL.AccountMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();
        
        public DTO.AccountMng.User GetUserInformation(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetUserInformation(userId, out notification);
        }

        public DTO.AccountMng.User GetUserInformationLight(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetUserInformationLight(userId, out notification);
        }

        public List<DTO.AccountMng.User> GetUserInformationWithFilter(int userId, Hashtable iFilters, string iSortedBy, string iSortedDirection, int iPageSize, int iPageIndex, out int oTotalRow, out Library.DTO.Notification notification)
        {
            //write log
            fwBLL.WriteLog(userId, 0, "Search User Profile");

            return factory.GetUserWithFilter(iFilters, iSortedBy, iSortedDirection, iPageSize, iPageIndex, out oTotalRow, out notification);
        }

        public bool IsAccountDisabled(string aspNetUserId)
        {
            return factory.IsAccountDisabled(aspNetUserId);
        }

        public bool IsNeedToChangePassword(string aspNetUserId)
        {
            return factory.IsNeedToChangePassword(aspNetUserId);
        }

        public void UpdateLastPasswordChange(string aspNetUserId)
        {
            factory.UpdateLastPasswordChange(aspNetUserId);
        }
    }
}
