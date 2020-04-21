using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
namespace DAL.ShowroomReceiptMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ShowroomReceiptMng.SearchFormData, DTO.ShowroomReceiptMng.EditFormData, DTO.ShowroomReceiptMng.ShowroomReceipt>
    {
        private DataConverter converter = new DataConverter();

        private ShowroomReceiptMngEntities CreateContext()
        {
            return new ShowroomReceiptMngEntities(DALBase.Helper.CreateEntityConnectionString("ShowroomReceiptMngModel"));
        }

        public override bool Approve(int id, ref DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        
        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Delete success" };
            try
            {
                using (ShowroomReceiptMngEntities context = CreateContext())
                {
                    var dbItem = context.ShowroomReceipt.Where(o => o.ShowroomReceiptID == id).FirstOrDefault();
                    if (dbItem == null)
                    {
                        notification.Message = "Deleted not success. Data not found";
                        return false;
                    }
                    else {
                        context.ShowroomReceipt.Remove(dbItem);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex){
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

        public override DTO.ShowroomReceiptMng.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.ShowroomReceiptMng.SearchFormData searchFormData = new DTO.ShowroomReceiptMng.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string ReceiptNo = string.Empty;
            string ShowroomNM = string.Empty;
            string StorekeeperName = string.Empty;
            string ImportFrom = string.Empty;
            string ExportTo = string.Empty;

            if (filters.ContainsKey("ReceiptNo")) ReceiptNo = filters["ReceiptNo"].ToString();
            if (filters.ContainsKey("ShowroomNM")) ShowroomNM = filters["ShowroomNM"].ToString();
            if (filters.ContainsKey("StorekeeperName")) StorekeeperName = filters["StorekeeperName"].ToString();
            if (filters.ContainsKey("ImportFrom")) ImportFrom = filters["ImportFrom"].ToString();
            if (filters.ContainsKey("ExportTo")) ExportTo = filters["ExportTo"].ToString();

            try
            {
                using (ShowroomReceiptMngEntities context = CreateContext())
                {
                    totalRows = context.ShowroomReceiptMng_action_SearchReceiptView(orderBy, orderDirection, ReceiptNo, ShowroomNM, StorekeeperName, ImportFrom, ExportTo).Count();
                    var result = context.ShowroomReceiptMng_action_SearchReceiptView(orderBy, orderDirection, ReceiptNo, ShowroomNM, StorekeeperName, ImportFrom, ExportTo);
                    searchFormData.Data = converter.DB2DTO_ShowroomReceiptSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                return searchFormData;
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
                return searchFormData;
            }
        }

        public override DTO.ShowroomReceiptMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.ShowroomReceiptMng.EditFormData editFormData = new DTO.ShowroomReceiptMng.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShowroomReceiptMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        ShowroomReceiptMng_ShowroomReceipt_View dbItem;
                        dbItem = context.ShowroomReceiptMng_ShowroomReceipt_View
                            .Include("ShowroomReceiptMng_ShowroomReceiptDetail_View")
                            .FirstOrDefault(o => o.ShowroomReceiptID == id);

                        editFormData.Data = converter.DB2DTO_ShowroomReceipt(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.ShowroomReceiptMng.ShowroomReceipt();
                        editFormData.Data.ShowroomReceiptDetails = new List<DTO.ShowroomReceiptMng.ShowroomReceiptDetail>();
                        editFormData.Data.Season = DALBase.Helper.GetCurrentSeason();
                        editFormData.Data.ReceiptDate = DateTime.Now.ToString("dd/MM/yyyy"); ;
                    }
                    //get support list
                    DAL.Support.DataFactory support_factory = new Support.DataFactory();
                    editFormData.Users = support_factory.GetUser().ToList();
                    editFormData.Showrooms = support_factory.GetShowroom();
                    editFormData.Seasons = support_factory.GetSeason().ToList();
                    return editFormData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return editFormData;
            }
        }

        public override bool UpdateData(int id, ref DTO.ShowroomReceiptMng.ShowroomReceipt dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShowroomReceiptMngEntities context = CreateContext())
                {
                    ShowroomReceipt dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ShowroomReceipt();
                        context.ShowroomReceipt.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ShowroomReceipt.FirstOrDefault(o => o.ShowroomReceiptID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        //if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        //{
                        //    throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        //}

                        if (dtoItem.ReceiptTypeID == 2) // 2: EXPORT
                        {
                            DAL.Support.DataFactory support_factory = new Support.DataFactory();
                            Hashtable support_filters = new Hashtable();
                            foreach (var item in dtoItem.ShowroomReceiptDetails)
                            {
                                if (item.ShowroomReceiptDetailID > 0)
                                {
                                    foreach (var area in item.ShowroomReceiptAreaDetails)
                                    {
                                        if (area.ShowroomReceiptAreaDetailID > 0)
                                        {
                                            var physicalByArea = support_factory.QuickSearchShowroomAreaByPhysicalQnt(support_filters, out notification).Where(o => o.ShowroomItemID == item.ShowroomItemID && o.ShowroomAreaID == area.ShowroomAreaID).FirstOrDefault();
                                            var currentArea = context.ShowroomReceiptAreaDetail.Where(o => o.ShowroomReceiptAreaDetailID == area.ShowroomReceiptAreaDetailID).FirstOrDefault();
                                            if (physicalByArea == null)
                                            {
                                                throw new Exception("Could not find this product in area. You should select another area");
                                            }
                                            else if (area.Quantity - currentArea.Quantity > physicalByArea.Quantity)
                                            {
                                                throw new Exception("Quantity must be less than current physical quantity");
                                            }
                                        }
                                        else
                                        {
                                            var physicalByArea = support_factory.QuickSearchShowroomAreaByPhysicalQnt(support_filters, out notification).Where(o => o.ShowroomItemID == item.ShowroomItemID && o.ShowroomAreaID == area.ShowroomAreaID).FirstOrDefault();
                                            if (physicalByArea == null)
                                            {
                                                throw new Exception("Could not find this product in area. You should select another area");
                                            }
                                            else if (area.Quantity > physicalByArea.Quantity)
                                            {
                                                throw new Exception("Quantity must be less than current physical quantity");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var area in item.ShowroomReceiptAreaDetails)
                                    {
                                        var physicalByArea = support_factory.QuickSearchShowroomAreaByPhysicalQnt(support_filters, out notification).Where(o => o.ShowroomItemID == item.ShowroomItemID && o.ShowroomAreaID == area.ShowroomAreaID).FirstOrDefault();
                                        if (physicalByArea == null)
                                        {
                                            throw new Exception("Could not find this product in area. You should select another area");
                                        }
                                        else if (area.Quantity > physicalByArea.Quantity)
                                        {
                                            throw new Exception("Quantity must be less than current physical quantity");
                                        }
                                    }
                                }
                            }
                        }

                        //convert dto to db
                        converter.DTO2DB_ShowroomReceipt(dtoItem, ref dbItem);
                        //reove orphan
                        context.ShowroomReceiptAreaDetail.Local.Where(o => o.ShowroomReceiptDetail == null).ToList().ForEach(o => context.ShowroomReceiptAreaDetail.Remove(o));
                        context.ShowroomReceiptDetail.Local.Where(o => o.ShowroomReceipt == null).ToList().ForEach(o => context.ShowroomReceiptDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //update receipt no
                        context.ShowroomReceiptMng_function_GenerateReceipNo(dbItem.ShowroomReceiptID,dbItem.Season,dbItem.ReceiptTypeID);
                        //get return data
                        dtoItem = GetData(dbItem.ShowroomReceiptID, out notification).Data;
                        return true;
                    }
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

        
    }
}
