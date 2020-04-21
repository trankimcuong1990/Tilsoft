using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class TutorialsMng : Lib.BLLBase2<DTO.Tutorials.SearchFormData, DTO.Tutorials.EditFormData, DTO.Tutorials.Tutorials  >
    {
        private DAL.TutorialsMng.DataFactory factory = new DAL.TutorialsMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();


        public override DTO.Tutorials.SearchFormData GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {


            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.Tutorials.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get Tutorial " + id.ToString());

            // query data
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete Tutorial " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.Tutorials.Tutorials dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
           // fwBLL.WriteLog(iRequesterID, 0, "update city " + id.ToString());

            // query data
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.Tutorials.Tutorials dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.Tutorials.Tutorials dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
       
    }
}


