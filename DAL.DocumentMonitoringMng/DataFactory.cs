using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.IO;
namespace DAL.DocumentMonitoringMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.DocumentMonitoringMng.DocumentMonitoringSearch, DTO.DocumentMonitoringMng.DocumentMonitoring>
    {
        private DataConverter converter = new DataConverter();
        
        private DocumentMonitoringMngEntities CreateContext()
        {
            return new DocumentMonitoringMngEntities(DALBase.Helper.CreateEntityConnectionString("DocumentMonitoringMngModel"));
        }
        
        public DataFactory(){}
        
        public override IEnumerable<DTO.DocumentMonitoringMng.DocumentMonitoringSearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = string.Empty;
            string InvoiceNo = string.Empty;
            string BLNo = string.Empty;

            if (filters.ContainsKey("Season")) Season = filters["Season"].ToString();
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();

            //try to get data
            try
            {
                using (DocumentMonitoringMngEntities context = CreateContext())
                {
                    totalRows = context.DocumentMonitoringMng_function_SearchDocumentMonitoring(orderBy, orderDirection, Season, InvoiceNo, BLNo).Count();
                    var result = context.DocumentMonitoringMng_function_SearchDocumentMonitoring(orderBy, orderDirection, Season, InvoiceNo, BLNo);
                    return converter.DB2DTO_DocumentMonitoringSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.DocumentMonitoringMng.DocumentMonitoringSearch>();
            }
        }

        public override DTO.DocumentMonitoringMng.DocumentMonitoring GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try to get data
            try
            {
                using (DocumentMonitoringMngEntities context = CreateContext())
                {
                    DTO.DocumentMonitoringMng.DocumentMonitoring dtoItem = new DTO.DocumentMonitoringMng.DocumentMonitoring();

                    DocumentMonitoringMng_DocumentMonitoring_View dbItem;
                    dbItem = context.DocumentMonitoringMng_DocumentMonitoring_View.FirstOrDefault(o => o.DocumentMonitoringID == id);
                    dtoItem = converter.DB2DTO_DocumentMonitoring(dbItem);
                    return dtoItem;
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
                return new DTO.DocumentMonitoringMng.DocumentMonitoring();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DocumentMonitoringMngEntities context = CreateContext())
                {
                    DocumentMonitoring dbItem = context.DocumentMonitoring.FirstOrDefault(o => o.DocumentMonitoringID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "document not found!";
                        return false;
                    }
                    else
                    {
                        context.DocumentMonitoring.Remove(dbItem);
                        context.SaveChanges();
                        return true;
                    }
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
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.DocumentMonitoringMng.DocumentMonitoring dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DocumentMonitoringMngEntities context = CreateContext())
                {
                    DocumentMonitoring dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new DocumentMonitoring();
                        context.DocumentMonitoring.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.DocumentMonitoring.FirstOrDefault(o => o.DocumentMonitoringID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        //convert dto to db
                        converter.DTO2DB_DocumentMonitoring(dtoItem, ref dbItem);
                        context.SaveChanges();

                        //Get return data
                        dtoItem = GetData(dbItem.DocumentMonitoringID, out notification);
                        return true;
                    }
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
                return false;
            }
        }

        public bool QuickUpdateData(int UserID, ref List<DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate> dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DocumentMonitoringMngEntities context = CreateContext())
                {
                    foreach (DTO.DocumentMonitoringMng.DocumentMonitoringSearchUpdate item in dtoItem)
                    {
                        DocumentMonitoring dbItem = null;
                        dbItem = context.DocumentMonitoring.FirstOrDefault(o => o.DocumentMonitoringID == item.DocumentMonitoringID);


                        if (dbItem == null)
                        {
                            notification.Message = "Data not found!";
                            return false;
                        }
                        else
                        {
                            if (item.IsEdit == 1)
                            {
                                converter.DTO2DB_QuickDocumentMonitoring(item, ref dbItem);
                                dbItem.UpdatedBy = UserID;
                                dbItem.UpdatedDate = DateTime.Now;
                            }
                            context.SaveChanges();
                        }
                    }
                    return true;
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
                return false;
            }
        }

        public DTO.DocumentMonitoringMng.SearchSupportList GetSearchSupportData()
        {
            DAL.Support.DataFactory factory = new Support.DataFactory();  
            DTO.DocumentMonitoringMng.SearchSupportList dtoSupport = new DTO.DocumentMonitoringMng.SearchSupportList();
            dtoSupport.Seasons = factory.GetSeason().ToList();
            return dtoSupport;
        }

        public DTO.DocumentMonitoringMng.EditSupportList GetEditSupportData()
        {
            DAL.Support.DataFactory support_factory = new Support.DataFactory();
            DTO.DocumentMonitoringMng.EditSupportList dtoSupport = new DTO.DocumentMonitoringMng.EditSupportList();
            using (DocumentMonitoringMngEntities context = CreateContext())
            {
                dtoSupport.DefaultRemarks = converter.DB2DTO_DefaultRemark(context.List_DocumentMonitoringRemark_View.ToList());
                dtoSupport.Users = support_factory.GetUser().ToList();
            }
            return dtoSupport;
        }

    }
}
