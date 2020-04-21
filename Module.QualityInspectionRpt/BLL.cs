using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.QualityInspectionRpt
{
    internal class BLL
    {
        private readonly DAL.DataFactory factory = new DAL.DataFactory();
        private readonly Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public object GetDataSupport(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwFactory.WriteLog(iRequesterID, 0, "Get data support");
            return factory.GetDataSupport(filters, out notification);
        }

        public object UpdateDataSupport(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwFactory.WriteLog(iRequesterID, 0, "Update data support");
            return factory.UpdateDataSupport(filters, out notification);
        }

        public object DeleteDataSupport(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwFactory.WriteLog(iRequesterID, 0, "Delete data support");
            return factory.DeleteDataSupport(filters, out notification);
        }

        public object GetInitData(int iRequesterID, out Notification notification)
        {
            fwFactory.WriteLog(iRequesterID, 0, "Get support Quality Inspection");
            return factory.GetInitData(out notification);
        }

        public object GetData(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwFactory.WriteLog(iRequesterID, 0, "Get Quality Inspection");
            return factory.GetData(filters, out notification);
        }

        public object UpdateData(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwFactory.WriteLog(iRequesterID, 0, "Update data Quality Inspection");

            return factory.UpdateData(iRequesterID, filters, out notification);
        }

        public object DeleteData(int iRequesterID, Hashtable filters, out Notification notification)
        {
            fwFactory.WriteLog(iRequesterID, 0, "Delete data of Quality Inspection");
            return factory.DeleteData(iRequesterID, filters, out notification);
        }
    }
}
