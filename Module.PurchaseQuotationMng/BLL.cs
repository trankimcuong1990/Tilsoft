using Library.DTO;
using System.Collections;

namespace Module.PurchaseQuotationMng
{
    public class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();

        #region ** Code developer Luu Nhut **

        public DTO.SearchDataDTO GetDataWithFilter(int iReiRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public DTO.EditDataDTO GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {

            return factory.GetData(id, out notification);
        }

        public bool UpdateData(int iRequesterID, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(iRequesterID, id, ref dtoItem, out notification);
        }

        public bool DeleteData(int id, out Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }
        #endregion

        #region ** Code developer Truong Son **

        public DTO.InitDefaultPriceDTO GetInitDefaultPrice(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.GetInitDefaultPrice(filters, out notification);
        }

        #endregion

        public object PreparingDefaultPriceData(int userID, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.PreparingDefaultPriceData(userID, filters, out notification);
        }

        public object SetDefaultPrice(int userID, Hashtable filters, out Notification notification)
        {
            return factory.SetDefaultPrice(userID, filters, out notification);
        }

        public bool ApproveData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Approve(userId, id, ref dtoItem, out notification);
        }

        public object GetProductionItemDefaultPrice(int userId, string searchString, out Notification notification)
        {
            return factory.GetProductionItemDefaultPrice(userId, searchString, out notification);
        }

        public object GetInitDataDefaultPrice(int iRequesterID, out Notification notification)
        {
            return factory.GetInitDataDefaultPrice(iRequesterID, out notification);
        }

        public object GetInitData(int iRequesterID, out Notification notification)
        {
            return factory.GetInitData(out notification);
        }

        public object GetContentPurchasingPriceFactory(int userID, Hashtable filters, out Notification notification)
        {
            return factory.GetContentPurchasingPriceFactory(userID, filters, out notification);
        }

        public object GetInitForm(int iRequesterID, out Notification notification)
        {
            return factory.GetInitForm(out notification);
        }

        public object GetMaterial(int iRequesterID, Hashtable filters, out Notification notification)
        {
            return factory.GetMaterial(iRequesterID, filters, out notification);
        }

        public object GetPurchasingPriceFactory(int iRequesterID, Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Notification notification)
        {
            return factory.GetPurchasingPriceFactory(iRequesterID, filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }
        public bool Cancel(int userId, int id, ref object dtoItem, out Notification notification)
        {
            return factory.Cancel(userId, id, ref dtoItem, out notification);
        }
    }
}
