using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProfileMng
{
    internal class BLL
    {
        private DAL.DataFactory factory = new DAL.DataFactory();
        private Framework.BLL fwBLL = new Framework.BLL();

        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            return factory.GetData(id, out notification);
        }

        public bool UpdateAccount(int iRequesterID, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateAccount(iRequesterID, id, dtoItem, out notification);
        }

        public bool UpdateEmployee(int iRequesterID, int id, object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateEmployee(iRequesterID, id, dtoItem, out notification);
        }

        public bool GetAPIKey(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetAPIKey(userId, out notification);
        }
    }
}
