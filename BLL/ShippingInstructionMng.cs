using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ShippingInstructionMng : Lib.BLLBase2<DTO.ShippingInstructionMng.SearchFormData, DTO.ShippingInstructionMng.EditFormData, DTO.ShippingInstructionMng.ShippingInstruction>
    {
        private DAL.ShippingInstructionMng.DataFactory factory = new DAL.ShippingInstructionMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override DTO.ShippingInstructionMng.SearchFormData GetDataWithFilter(int iRequesterID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of shipping instruction");

            // query data
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.ShippingInstructionMng.EditFormData GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get shipping instruction " + id.ToString());

            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete shipping instruction " + id.ToString());

            // query data
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.ShippingInstructionMng.ShippingInstruction dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update shipping instruction " + id.ToString());

            // query data
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public override bool Approve(int id, ref DTO.ShippingInstructionMng.ShippingInstruction dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "approve shipping instruction " + id.ToString());

            // query data
            return factory.Approve(id, ref dtoItem, out notification);
        }

        public override bool Reset(int id, ref DTO.ShippingInstructionMng.ShippingInstruction dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "reset shipping instruction " + id.ToString());

            // query data
            return factory.Reset(id, ref dtoItem, out notification);
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.ShippingInstructionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            return factory.GetFilterData(out notification);
        }

        public DTO.ShippingInstructionMng.ShippingInstruction GetDefault(int podid, out Library.DTO.Notification notification)
        {
            return factory.GetDefault(podid, out notification);
        }

        public DTO.ShippingInstructionMng.PODCountry GetCountryFromPODfunction(int clientId, out Library.DTO.Notification notification)
        {
            return factory.GetCountryFromPODfunction(clientId, out notification);
        }

        public string ExportExcel (System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.ExportExcel(filters, out notification);
        }
    }
}
