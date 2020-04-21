using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
namespace DAL.PackingListMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.PackingListMng.PackingListSearchResult, DTO.PackingListMng.PackingList>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        
        private PackingListMngEntities CreateContext()
        {
            return new PackingListMngEntities(DALBase.Helper.CreateEntityConnectionString("PackingListMngModel"));
        }

        public override IEnumerable<DTO.PackingListMng.PackingListSearchResult> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string PackingListUD = string.Empty;
            string InvoiceNo = string.Empty;
            string BLNo = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;
            string ContainerNo = string.Empty;

            int? iRequesterID = (int)filters["iRequesterID"];
            if (filters.ContainsKey("PackingListUD")) PackingListUD = filters["PackingListUD"].ToString();
            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            if (filters.ContainsKey("ContainerNo")) ContainerNo = filters["ContainerNo"].ToString();
            
            //try to get data
            try
            {
                using (PackingListMngEntities context = CreateContext())
                {
                    totalRows = context.PackingListMng_function_SearchPackingList(  iRequesterID,
                                                                                    orderBy,
                                                                                    orderDirection,
                                                                                    PackingListUD,
                                                                                    InvoiceNo,
                                                                                    BLNo,
                                                                                    ClientUD,
                                                                                    ClientNM,
                                                                                    ContainerNo
                                                                                    ).Count();

                    var result = context.PackingListMng_function_SearchPackingList( iRequesterID,
                                                                                    orderBy,
                                                                                    orderDirection,
                                                                                    PackingListUD,
                                                                                    InvoiceNo,
                                                                                    BLNo,
                                                                                    ClientUD,
                                                                                    ClientNM,
                                                                                    ContainerNo
                                                                                    );
                    
                    return converter.DB2DTO_PackingListSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                return new List<DTO.PackingListMng.PackingListSearchResult>();
            }
        }

        public override DTO.PackingListMng.PackingList GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (PackingListMngEntities context = CreateContext())
                {
                    PackingListMng_PackingList_View dbItem;
                    dbItem = context.PackingListMng_PackingList_View
                        .FirstOrDefault(o => o.PackingListID == id);
                    return converter.DB2DTO_PackingList(dbItem);
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
                return new DTO.PackingListMng.PackingList();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int id, ref DTO.PackingListMng.PackingList dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        /*
         * Custom functions
         */
        
        public DTO.PackingListMng.DataContainer GetDataContainer(int id, int purchasingInvoiceID, int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (PackingListMngEntities context = CreateContext())
                {
                    DTO.PackingListMng.DataContainer dtoItem = new DTO.PackingListMng.DataContainer();

                    if (id > 0)
                    {
                        //check permission on invoice
                        if (fwFactory.CheckPackingListPermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this packing list");
                        }
                        
                        PackingListMng_PackingList_View dbItem;
                        dbItem = context.PackingListMng_PackingList_View
                                .Include("PackingListMng_PackingListDetail_View")
                                .Include("PackingListMng_PackingListDetailExtend_View")
                                .Include("PackingListMng_ECommercialInvoice_View")
                                .Include("PackingListMng_PackingListSparepartDetail_View")
                                .FirstOrDefault(o => o.PackingListID == id);
                        DTO.PackingListMng.PackingList PackingListDTO = converter.DB2DTO_PackingList(dbItem);
                        dtoItem.PackingListData = PackingListDTO;
                    }
                    else
                    {
                        //check permission on invoice
                        if (fwFactory.CheckPurchasingInvoicePermission(iRequesterID, purchasingInvoiceID) == 0)
                        {
                            throw new Exception("You do not have access permission on this invoice to create packing list");
                        }
                        dtoItem.PackingListData = new DTO.PackingListMng.PackingList();
                        //init data
                        PackingListMng_InitInfo_View dbInit = context.PackingListMng_InitInfo_View.Include("PackingListMng_InitInfoDetail_View").Include("PackingListMng_InitInfoSparepartDetail_View").FirstOrDefault(o => o.PurchasingInvoiceID == purchasingInvoiceID);
                        dtoItem.PackingListData = converter.DB2DTO_InitInfo(dbInit);
                        
                        //init other info
                        dtoItem.PackingListData.PackingListUD = dbInit.InvoiceNo;
                        dtoItem.PackingListData.PackingListDateFormated = DateTime.Now.ToString("dd/MM/yyyy");

                        int i = -1;
                        foreach (var item in dtoItem.PackingListData.PackingListDetails)
                        {
                            item.PackingListDetailID = i;
                            i--;
                        }

                        i = -1;
                        foreach (var item in dtoItem.PackingListData.PackingListSparepartDetails)
                        {
                            item.PackingListSparepartDetailID = i;
                            i--;
                        }

                        dtoItem.PackingListData.PackingListDetailExtends = new List<DTO.PackingListMng.PackingListDetailExtend>();
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
                return new DTO.PackingListMng.DataContainer();
            }
        }

        public IEnumerable<DTO.PackingListMng.InitInfo> GetInitInfos(System.Collections.Hashtable filters, int iRequesterID, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            string BLNo = string.Empty;

            if (filters.ContainsKey("BLNo")) BLNo = filters["BLNo"].ToString();
            //try to get data
            try
            {
                using (PackingListMngEntities context = CreateContext())
                {
                    List<int?> supplierIDs = fwFactory.GetListSupplierByUser(iRequesterID);
                    //totalRows = context.PackingListMng_InitInfo_View.Count();
                    var result = context.PackingListMng_InitInfo_View.Where(o => supplierIDs.Contains(o.SupplierID) && o.BLNo.Contains(BLNo));
                    return converter.DB2DTO_InitInfos(result.ToList());
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
                return new List<DTO.PackingListMng.InitInfo>();
            }
        }

        public bool UpdateData(int id, ref DTO.PackingListMng.PackingList dtoItem, int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PackingListMngEntities context = CreateContext())
                {
                    PackingList dbItem = null;
                    if (id == 0)
                    {
                        //check permission on invoice
                        if (fwFactory.CheckPurchasingInvoicePermission(iRequesterID, dtoItem.PurchasingInvoiceID.Value) == 0)
                        {
                            throw new Exception("You do not have access permission on this invoice to create packing list");
                        }
                        dbItem = new PackingList();
                        context.PackingList.Add(dbItem);
                    }
                    else
                    {
                        //check permission on invoice
                        if (fwFactory.CheckPackingListPermission(iRequesterID, id) == 0)
                        {
                            throw new Exception("You do not have access permission on this packing list");
                        }
                        dbItem = context.PackingList.FirstOrDefault(o => o.PackingListID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB_PackingList(dtoItem, ref dbItem);

                        if (id == 0)
                        {
                            dbItem.CreatedBy = dtoItem.UpdatedBy;
                            dbItem.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            dbItem.UpdatedBy = dtoItem.UpdatedBy;
                            dbItem.UpdatedDate = DateTime.Now;
                        }

                        context.PackingListDetail.Local.Where(o => o.PackingList == null).ToList().ForEach(o => context.PackingListDetail.Remove(o));
                        context.PackingListSparepartDetail.Local.Where(o => o.PackingList == null).ToList().ForEach(o => context.PackingListSparepartDetail.Remove(o));
                        context.PackingListDetailExtend.Local.Where(o => o.PackingList == null).ToList().ForEach(o => context.PackingListDetailExtend.Remove(o));

                        context.SaveChanges();
                        dtoItem = GetData(dbItem.PackingListID, out notification);

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

        public bool DeleteData(int id, int iRequesterID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                //check permission on invoice
                if (fwFactory.CheckPackingListPermission(iRequesterID, id) == 0)
                {
                    throw new Exception("You do not have access permission on this packing list");
                }
                using (PackingListMngEntities context = CreateContext())
                {
                    PackingList dbItem = context.PackingList.FirstOrDefault(o => o.PackingListID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        List<PackingListDetail> need_delete_details = new List<PackingListDetail>();
                        foreach (PackingListDetail dbDetail in dbItem.PackingListDetail)
                        {
                            need_delete_details.Add(dbDetail);
                        }

                        List<PackingListDetailExtend> need_delete_detailextends = new List<PackingListDetailExtend>();
                        foreach (PackingListDetailExtend dbDetailExtend in dbItem.PackingListDetailExtend)
                        {
                            need_delete_detailextends.Add(dbDetailExtend);
                        }

                        foreach (var item in need_delete_details)
                        {
                            context.PackingListDetail.Remove(item);
                        }
                        foreach (var item in need_delete_detailextends)
                        {
                            context.PackingListDetailExtend.Remove(item);
                        }

                        context.PackingList.Remove(dbItem);
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

        

    }
}
