using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
namespace DAL.TransferShowroomAreaMng
{
    public class DataFactory : DALBase.FactoryBase2<List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>, DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch, DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>
    {
        private DataConverter converter = new DataConverter();
        private string _TempFolder;

        public DataFactory() { }

        private TransferShowroomAreaMngEntities CreateContext()
        {
            return new TransferShowroomAreaMngEntities(DALBase.Helper.CreateEntityConnectionString("TransferShowroomAreaMngModel"));
        }

        public override bool Approve(int id, ref DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {            
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ArticleCode = null;
            string Description = null;
            int? FromShowroomAreaID = null;
            int? ToShowroomAreaID = null;

            if (filters.ContainsKey("ArticleCode") && filters["ArticleCode"]!= null) ArticleCode = filters["ArticleCode"].ToString();
            if (filters.ContainsKey("Description") && filters["Description"]!=null) Description = filters["Description"].ToString();
            if (filters.ContainsKey("Description") && filters["Description"] != null) Description = filters["Description"].ToString();
            if (filters.ContainsKey("FromShowroomAreaID") && filters["FromShowroomAreaID"] != null) FromShowroomAreaID = Convert.ToInt32(filters["FromShowroomAreaID"]);
            if (filters.ContainsKey("ToShowroomAreaID") && filters["ToShowroomAreaID"] != null) ToShowroomAreaID = Convert.ToInt32(filters["ToShowroomAreaID"]);

            try
            {
                using (TransferShowroomAreaMngEntities context = CreateContext())
                {
                    totalRows = context.TransferShowroomAreaMng_function_SearchTransferShowroomArea(orderBy, orderDirection, ArticleCode, Description,FromShowroomAreaID,ToShowroomAreaID).Count();
                    var result = context.TransferShowroomAreaMng_function_SearchTransferShowroomArea(orderBy, orderDirection, ArticleCode, Description, FromShowroomAreaID, ToShowroomAreaID);
                    return converter.DB2DTO_TransferShowroomAreaSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return new List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>();
            }
        }

        public override DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int id, ref DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Transfer success" };
            try
            {
                if (!dtoItem.ShowroomItemID.HasValue || !dtoItem.FromShowroomAreaID.HasValue || !dtoItem.ToShowroomAreaID.HasValue || !dtoItem.Quantity.HasValue)
                {
                    throw new Exception("Please fill-in fields before transfer");
                }

                if (dtoItem.FromShowroomAreaID == dtoItem.ToShowroomAreaID)
                {
                    throw new Exception("Can not transfer. From Area have to difference with To Area");
                }

                Hashtable filter = new Hashtable();
                int? showroomItemID = dtoItem.ShowroomItemID;
                int? showroomAreaID = dtoItem.FromShowroomAreaID;
                var physicalByArea = new DAL.Support.DataFactory().QuickSearchShowroomAreaByPhysicalQnt(filter, out notification).Where(o => o.ShowroomItemID == showroomItemID && o.ShowroomAreaID == showroomAreaID).FirstOrDefault();

                if (physicalByArea == null)
                {
                    throw new Exception("Could not find this product in area. You should select another area");
                }
                else if (dtoItem.Quantity > physicalByArea.Quantity)
                {
                    throw new Exception("Quantity must be less than current physical quantity");
                }
                
                using (TransferShowroomAreaMngEntities context = CreateContext())
                {
                    TransferShowroomArea dbItem = new TransferShowroomArea();
                    converter.DTO2DB_TransferArea(dtoItem, ref dbItem);
                    context.TransferShowroomArea.Add(dbItem);
                    context.SaveChanges();
                    dtoItem = converter.DB2DTO_TransferShowroomArea(context.TransferShowroomAreaMng_TransferShowroomArea_SearchView.Where(o => o.TransferShowroomAreaID == dbItem.TransferShowroomAreaID).FirstOrDefault());
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

        public  bool TransferMultiItem(List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> dtoItem,out List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch> errorItems, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Transfer success" };
            errorItems = new List<DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch>();
            try
            {
                TransferShowroomArea dbTransfer;
                Hashtable filter = new Hashtable();
                DAL.Support.DataFactory support_factory = new Support.DataFactory();

                var dtoItemGroup = from p in dtoItem group p by new { p.ShowroomItemID, p.FromShowroomAreaID } into g select new { g.Key.ShowroomItemID, g.Key.FromShowroomAreaID, TotalTransferQnt = g.Sum(x => x.Quantity) };
                foreach (var item in dtoItemGroup)
                {
                    var physicalByArea = new DAL.Support.DataFactory().QuickSearchShowroomAreaByPhysicalQnt(filter, out notification).Where(o => o.ShowroomItemID == item.ShowroomItemID && o.ShowroomAreaID == item.FromShowroomAreaID).FirstOrDefault();
                    if (physicalByArea == null)
                    {
                        DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch errorItem = new DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch();
                        errorItem.ShowroomItemID = item.ShowroomItemID;
                        errorItem.ToShowroomAreaID = item.FromShowroomAreaID;
                        errorItems.Add(errorItem);
                        throw new Exception("Could not find this product in area. You should select another area");
                    }
                    else if (item.TotalTransferQnt > physicalByArea.Quantity)
                    {
                        DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch errorItem = new DTO.TransferShowroomAreaMng.TransferShowroomAreaSearch();
                        errorItem.ShowroomItemID = item.ShowroomItemID;
                        errorItem.ToShowroomAreaID = item.FromShowroomAreaID;
                        errorItems.Add(errorItem);
                        throw new Exception("Quantity must be less than current physical quantity");
                    }
                }

                foreach (var item in dtoItem)
                {
                    if (!item.ToShowroomAreaID.HasValue)
                    {
                        throw new Exception("Can not transfer. You have to select To Area");
                    }

                    if (item.FromShowroomAreaID == item.ToShowroomAreaID)
                    {
                        throw new Exception("Can not transfer. From Area have to difference with To Area");
                    }
                }
                
                using (TransferShowroomAreaMngEntities context = CreateContext())
                {
                   foreach (var item in dtoItem)
                    {
                        dbTransfer = new TransferShowroomArea();
                        converter.DTO2DB_TransferArea(item, ref dbTransfer);
                        context.TransferShowroomArea.Add(dbTransfer);
                    }
                   context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return false;
            }
        }

       
    }
}
