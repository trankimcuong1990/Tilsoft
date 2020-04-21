using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BLL
{
    public class DocumentClientMng : Lib.BLLBase<DTO.DocumentClientMng.DocumentClientSearchResult, DTO.DocumentClientMng.DocumentClient>
    {
        private DAL.DocumentClientMng.DataFactory factory = new DAL.DocumentClientMng.DataFactory();
        private BLL.Framework fwBLL = new Framework();

        public override IEnumerable<DTO.DocumentClientMng.DocumentClientSearchResult> GetDataWithFilter(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of document client");
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public override DTO.DocumentClientMng.DocumentClient GetData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get document client " + id.ToString());
            return factory.GetData(id, out notification);
        }

        public override bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "delete document client " + id.ToString());
            return factory.DeleteData(id, out notification);
        }

        public override bool UpdateData(int id, ref DTO.DocumentClientMng.DocumentClient dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "update document client " + id.ToString());
            dtoItem.UpdatedBy = iRequesterID;
            return factory.UpdateData(id, ref dtoItem, out notification);
        }

        public bool QuickUpdateData(ref List<DTO.DocumentClientMng.DocumentClientSearchUpdate> dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "update document client");
            return factory.QuickUpdateData(iRequesterID, ref dtoItem, out notification);
        }

        public DTO.DocumentClientMng.DataContainer GetDataContainer(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get Model " + id.ToString());

            // query data
            return factory.GetDataContainer(id, out notification);
        }

        public bool AddSelectorForeignKey(ref DTO.DocumentClientMng.ForeignKeyTable dtoItem,out Library.DTO.Notification notification)
        {
            int ID = 0;
            bool result = false;
            notification = new Library.DTO.Notification();
            if (dtoItem.Foreign_Type == 1)
            {
                result = factory.Add_TypeOfDelivery(dtoItem.Foreign_NM, out ID, out notification);
            }
            else if (dtoItem.Foreign_Type == 2)
            {
                result = factory.Add_PlaceOfBarge(dtoItem.Foreign_NM, out ID, out notification);
            }
            else if (dtoItem.Foreign_Type == 3)
            {
                result = factory.Add_PlaceOfDelivery(dtoItem.Foreign_NM, out ID, out notification);
            }
            else if (dtoItem.Foreign_Type == 4)
            {
                result = factory.Add_DeliveryStatus(dtoItem.Foreign_NM, out ID, out notification);
            }
            else if (dtoItem.Foreign_Type == 5)
            {
                result = factory.Add_PaymentStatus(dtoItem.Foreign_NM, out ID, out notification);
            }
            dtoItem.Foreign_ID = ID;
            return result;
        }

        public DTO.DocumentClientMng.DataSearchContainer SearchDataContainer(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get list of document client");
            // query data
            return factory.SearchDataContainer(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public string GetReportData(string season,int iRequesterID, out Library.DTO.Notification notification)
        {
            // keep log entry
            fwBLL.WriteLog(iRequesterID, 0, "get document client report");
            return factory.GetReportData(season,out notification);
        }
        
        public bool ConfirmDateContainerDelivery(int id, string dateContainerDelivery, int iRequesterID, out Library.DTO.Notification notification)
        {
            fwBLL.WriteLog(iRequesterID, 0, "confirm date container delivery");
            return factory.ConfirmDateContainerDelivery(id, dateContainerDelivery, iRequesterID, out notification);
        }
    }
}
   