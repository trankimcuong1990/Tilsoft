using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.WarehouseExportMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.WarehouseExportMng.SearchFormData, DTO.WarehouseExportMng.EditFormData, DTO.WarehouseExportMng.WarehouseExport>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();

        private WarehouseExportMngEntities CreateContext()
        {
            return new WarehouseExportMngEntities(DALBase.Helper.CreateEntityConnectionString("WareHouseExportMngModel"));
        }

        public override DTO.WarehouseExportMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.WarehouseExportMng.SearchFormData data = new DTO.WarehouseExportMng.SearchFormData();
            data.Data = new List<DTO.WarehouseExportMng.WarehouseExportSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (WarehouseExportMngEntities context = CreateContext())
                {
                    string receiptNo = null;
                    string crmNo = null;   
                    string clientUD = null;
                    string clientNM = null;
                    string articleCode = null;
                    string description = null;
                    string proformaInvoiceNo = null;

                    if (filters.ContainsKey("ReceiptNo") && !string.IsNullOrEmpty(filters["ReceiptNo"].ToString()))
                    {
                        receiptNo = filters["ReceiptNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        proformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        clientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientNM") && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
                    {
                        clientNM = filters["ClientNM"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("CRMNo") && !string.IsNullOrEmpty(filters["CRMNo"].ToString()))
                    {
                        crmNo = filters["CRMNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        articleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        description = filters["Description"].ToString().Replace("'", "''");
                    }

                    totalRows = context.WarehouseExportMng_function_SearchWarehouseExport(receiptNo, crmNo, clientUD, clientNM, articleCode, description, proformaInvoiceNo, orderBy, orderDirection).Count();
                    var result = context.WarehouseExportMng_function_SearchWarehouseExport(receiptNo, crmNo, clientUD, clientNM, articleCode, description, proformaInvoiceNo, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_WarehouseExportSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }

            return data;
        }

        public override DTO.WarehouseExportMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.WarehouseExportMng.EditFormData data = new DTO.WarehouseExportMng.EditFormData();
            data.Data = new DTO.WarehouseExportMng.WarehouseExport();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            if (id > 0)
            {
                try
                {
                    using (WarehouseExportMngEntities context = CreateContext())
                    {
                        data.Data = converter.DB2DTO_WarehouseExport(context.WarehouseExportMng_WarehouseExport_View.Include("WarehouseExportMng_WarehouseExportProductDetail_View").FirstOrDefault(o => o.WarehouseExportID == id));
                        data.Data.ConcurrencyFlag_String = Convert.ToBase64String(data.Data.ConcurrencyFlag);
                    }
                }
                catch (Exception ex)
                {
                    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                }
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseExportMngEntities context = CreateContext())
                {
                    WarehouseExport dbItem = context.WarehouseExport.FirstOrDefault(o => o.WarehouseExportID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Export receipt: " + dbItem.ReceiptNo + " not found!");
                    }
                    else
                    {
                        if (dbItem.ProcessingStatusID == 1)
                        {
                            context.WarehouseExport.Remove(dbItem);
                            context.SaveChanges();
                            result = true;
                        }
                        else
                        {
                            throw new Exception("Can not delete the approved/canceled export receipt");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }

            return result;
        }

        public override bool UpdateData(int id, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, out Library.DTO.Notification notification)
        {
            bool result = false;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseExportMngEntities context = CreateContext())
                {
                    WarehouseExport dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new WarehouseExport();
                        context.WarehouseExport.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.WarehouseExport.FirstOrDefault(o => o.WarehouseExportID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Export receipt not found!";
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // check if status = CONFIRMED
                        if (dbItem.ProcessingStatusID > 1)
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONFIRMED_CONFLICT);
                        }

                        DateTime dateValue;
                        if (DateTime.TryParse(dtoItem.ExportedDate, new System.Globalization.CultureInfo("vi-VN"), System.Globalization.DateTimeStyles.None, out dateValue))
                        {
                            dbItem.ExportedDate = dateValue;
                        }

                        converter.DTO2DB(dtoItem, ref dbItem);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphans
                        context.WarehouseExportProductDetail.Local.Where(o => o.WarehouseExport == null).ToList().ForEach(o => context.WarehouseExportProductDetail.Remove(o));
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.WarehouseExportID, out notification).Data;

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            }

            return result;
        }

        public override bool Approve(int id, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTIONS
        //
        public bool ChangeStatus(int id, int statusId, ref DTO.WarehouseExportMng.WarehouseExport dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            // only update the current data if the status is different than CONFIRMED
            if (dtoItem.ProcessingStatusID == 1)
            {
                // update current data
                if (!UpdateData(id, ref dtoItem, out notification))
                {
                    return false;
                }
            }

            try
            {
                using (WarehouseExportMngEntities context = CreateContext())
                {
                    WarehouseExport dbItem = context.WarehouseExport.FirstOrDefault(o => o.WarehouseExportID == id);
                    if (dbItem != null)
                    {
                        dbItem.ProcessingStatusID = statusId;
                        dbItem.StatusUpdatedBy = dtoItem.UpdatedBy;
                        dbItem.StatusUpdatedDate = DateTime.Now;
                        context.SaveChanges();

                        dtoItem = GetData(id, out notification).Data;

                        return true;
                    }
                    else
                    {
                        throw new Exception("Export receipt not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }
    }
}
