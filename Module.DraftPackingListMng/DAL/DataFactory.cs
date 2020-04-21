using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.DraftPackingListMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.DraftPackingListDTO>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private DraftPackingListMngEntities CreateContext()
        {
            return new DraftPackingListMngEntities(Library.Helper.CreateEntityConnectionString("DAL.DraftPackingListMngModel"));
        }

        public bool Delete(int id, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DraftPackingListMngEntities context = CreateContext())
                {
                    DraftPackingList dbItem = context.DraftPackingList.FirstOrDefault(o => o.DraftPackingListID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        List<DraftPackingListDetail> need_delete_details = new List<DraftPackingListDetail>();
                        foreach (DraftPackingListDetail dbDetail in dbItem.DraftPackingListDetail)
                        {
                            need_delete_details.Add(dbDetail);
                        }

                        foreach (var item in need_delete_details)
                        {
                            context.DraftPackingListDetail.Remove(item);
                        }

                        context.DraftPackingList.Remove(dbItem);
                        context.SaveChanges();

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

        public List<DTO.InitInfo> GetInitInfos(Hashtable filters, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            string invoiceNo = string.Empty;

            if (filters.ContainsKey("invoiceNo")) invoiceNo = filters["invoiceNo"].ToString();
            //try to get data
            try
            {
                using (DraftPackingListMngEntities context = CreateContext())
                {
                    //totalRows = context.DraftPackingList_InitInfo_View.Count();
                    var result = context.DraftPackingList_InitInfo_View.Where(o => o.InvoiceNo.Contains(invoiceNo)).ToList();
                    var data =  converter.DB2DTO_InitInfos(result);
                    return data;
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
                return new List<DTO.InitInfo>();
            }
        }

        public DTO.DataContainer GetData(Hashtable filters, out Notification notification)
        {
            int id = 0, pID = 0;
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            if (filters.ContainsKey("id"))
                id = int.Parse(filters["id"].ToString());
            if (filters.ContainsKey("param"))
                pID = int.Parse(filters["param"].ToString());

            //try to get data
            try
            {
                using (DraftPackingListMngEntities context = CreateContext())
                {
                    DTO.DataContainer dtoItem = new DTO.DataContainer();

                    if (id > 0)
                    {
                        DraftPackingList_DraftPackingList_View dbItem;
                        dbItem = context.DraftPackingList_DraftPackingList_View.FirstOrDefault(o => o.DraftPackingListID == id);
                        DTO.DraftPackingListDTO DraftPackingListDTO = converter.DB2DTO_DraftPackingList(dbItem);
                        dtoItem.DraftPackingListData = DraftPackingListDTO;
                    }
                    else // new draft
                    {
                        DraftPackingList_NewInfo_View dbInit = context.DraftPackingList_NewInfo_View.Include("DraftPackingList_NewInfoDetail_View").Include("DraftPackingList_NewInfoSparepartDetail_View").FirstOrDefault(o => o.DraftCommercialInvoiceID == pID);
                        dtoItem.DraftPackingListData = converter.DB2DTO_NewInfo(dbInit);

                        //dtoItem.DraftPackingListData.DraftPackingListDetail = converter.DB2DTO_DraftPackingListDetail(context.DraftPackingList_NewInfoDetail_View.Where(o => o.DraftCommercialInvoiceID == pID).ToList());
                        //dtoItem.DraftPackingListData.DraftPackingListSparepartDetail = converter.DB2DTO_DraftPackingListSparepartDetail(context.DraftPackingList_NewInfoSparepartDetail_View.Where(o => o.DraftCommercialInvoiceID == pID).ToList());

                        //DraftPackingList_NewInfo_View dbItem;
                        //dbItem = context.DraftPackingList_NewInfo_View.FirstOrDefault(o => o.DraftCommercialInvoiceID == pID);
                        //DTO.DraftPackingListDTO DraftPackingListDTO = converter.DB2DTO_NewInfo(dbItem);
                        //dtoItem.DraftPackingListData = DraftPackingListDTO;
                    }
                    return dtoItem;
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
                return new DTO.DataContainer();
            }

        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.DraftPackingListSearchResult>();
            totalRows = 0;

            string DraftPackingListUD = null;
            string InvoiceNo = null;
            string ClientUD = null;
            string ClientNM = null;
            if (filters.ContainsKey("DraftPackingListUD") && filters["DraftPackingListUD"] != null && !string.IsNullOrEmpty(filters["DraftPackingListUD"].ToString()))
            {
                DraftPackingListUD = filters["DraftPackingListUD"].ToString();
            }
            if (filters.ContainsKey("InvoiceNo") && filters["InvoiceNo"] != null && !string.IsNullOrEmpty(filters["InvoiceNo"].ToString()))
            {
                InvoiceNo = filters["InvoiceNo"].ToString();
            }
            if (filters.ContainsKey("ClientUD") && filters["ClientUD"] != null && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString();
            }
            if (filters.ContainsKey("ClientNM") && filters["ClientNM"] != null && !string.IsNullOrEmpty(filters["ClientNM"].ToString()))
            {
                ClientNM = filters["ClientNM"].ToString();
            }

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    totalRows = context.DraftPackingListMng_function_SearchDraftPackingList(DraftPackingListUD, InvoiceNo, ClientUD, ClientNM, orderBy, orderDirection).Count();
                    var result = context.DraftPackingListMng_function_SearchDraftPackingList(DraftPackingListUD, InvoiceNo, ClientUD, ClientNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_PackingListSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.DraftPackingListDTO GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };
            DTO.DraftPackingListDTO dtoDraftPackingList = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.DraftPackingListDTO>();
            try
            {
                using (DraftPackingListMngEntities context = CreateContext())
                {
                    DraftPackingList dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new DraftPackingList();
                        context.DraftPackingList.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.DraftPackingList.FirstOrDefault(o => o.DraftPackingListID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_DraftPackingList(dtoDraftPackingList, ref dbItem);

                        if (id == 0)
                        {
                            dbItem.CreatedBy = userId;
                            dbItem.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;
                        }

                        context.DraftPackingListDetail.Local.Where(o => o.DraftPackingList == null).ToList().ForEach(o => context.DraftPackingListDetail.Remove(o));
                       

                        context.SaveChanges();

                        Hashtable filter = new Hashtable() ;
                        filter["id"] = dbItem.DraftPackingListID;
                        dtoItem = GetData(filter, out notification);

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

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public string getDraftPackingListPrintOut(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DraftPackingListDataObject ds = new DraftPackingListDataObject();

            //try to get data
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("DraftPackingList_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Id", id);
                adap.TableMappings.Add("Table", "DraftPackingList");
                adap.TableMappings.Add("Table1", "DraftPackingListDetail");
                adap.Fill(ds);

                return Library.Helper.CreateReceiptPrintout(ds.Tables["DraftPackingList"], ds.Tables["DraftPackingListDetail"], "DraftPackingListPrintoutPDF.rdlc");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }
        }
    }
}
