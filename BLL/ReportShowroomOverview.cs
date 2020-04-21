using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace BLL
{
    public class ReportShowroomOverview
    {
        DAL.ReportShowroomOverview.DataFactory factory = new DAL.ReportShowroomOverview.DataFactory();
        public List<DTO.ReportShowroomOverview.ShowroomArea> GetReportShowroomOverview(Hashtable filters, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetShowroomOverview(filters, out notification);
        }

        public string PrinHangTag(string keyIDs, out Library.DTO.Notification notification)
        {
            return factory.PrintHangTag(keyIDs, out notification);
        }

        public string PrintShowroomOverview(out Library.DTO.Notification notification)
        {
            return factory.PrintShowroomOverview(out notification);
        }
    }
}
