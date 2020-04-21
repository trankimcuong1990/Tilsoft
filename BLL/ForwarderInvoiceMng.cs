using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ForwarderInvoiceMng : Lib.BLLBase2<DTO.ForwarderInvoiceMng.SearchFormData, DTO.ForwarderInvoiceMng.EditFormData, DTO.ForwarderInvoiceMng.ForwarderInvoice>
    {
        private DAL.ForwarderInvoiceMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();
        public ForwarderInvoiceMng(string tempFolder)
        {
            factory = new DAL.ForwarderInvoiceMng.DataFactory(tempFolder);
        }

        public override DTO.ForwarderInvoiceMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of forwarder invoice");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ForwarderInvoiceMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get forwarder invoice " + id.ToString());

            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete forwarder invoice " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ForwarderInvoiceMng.ForwarderInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update forwarder invoice " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public DTO.ForwarderInvoiceMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            //return factory.GetFilterData(out notification);
            throw new NotImplementedException();
        }

        public DTO.ForwarderInvoiceMng.InitFormData GetInitData(int iRequesterID, out Library.DTO.Notification notification)
        {
            //return factory.GetInitData(out notification);
            throw new NotImplementedException();
        }

        public override bool Approve(int id, ref DTO.ForwarderInvoiceMng.ForwarderInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ForwarderInvoiceMng.ForwarderInvoice dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
