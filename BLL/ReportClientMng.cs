using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportClientMng
    {
         private BLL.Framework fwBLL = new Framework();
        private DAL.ReportClientMng.DataFactory   factory = new DAL.ReportClientMng.DataFactory();

        public string GetReportClients(int iRequesterID, out Library.DTO.Notification notification)
        {
          
            fwBLL.WriteLog(iRequesterID, 0, "get report Clients");

           
            return factory.GetReportClients( iRequesterID, out notification);
        }

       
    }
    }

