using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class CreditNoteMng : Lib.BLLBase<DTO.CreditNoteMng.CreditNote, DTO.CreditNoteMng.CreditNote>
    {
        private DAL.CreditNoteMng.DataFactory factory;
        private BLL.Framework fwBLL = new Framework();

        public CreditNoteMng()
        {
            factory = new DAL.CreditNoteMng.DataFactory();
        }

        public override IEnumerable<DTO.CreditNoteMng.CreditNote> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.CreditNoteMng.CreditNote GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get credit note" + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "delete credit note" + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.CreditNoteMng.CreditNote dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update CreditNote " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public  DTO.CreditNoteMng.CreditNote GetEditData(int id,int eCommercialInvoice, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "get credit note" + id.ToString());
            return factory.GetEditData(id, eCommercialInvoice, out notification);
        }

        public DTO.ECommercialInvoiceMng.InvoiceContainerPrintout GetCreditNotePrintout(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            return factory.GetCreditNotePrintout(id, out  notification);
        }

        public bool Confirm(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "confirm credit note");
            return factory.Confirm(id, iRequesterID, out notification);
        }
    }
}
