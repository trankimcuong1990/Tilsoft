using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace DAL.WarehouseCIMng
{
    public class DataFactory : DALBase.FactoryBase<DTO.WarehouseCIMng.WarehouseCISearch, DTO.WarehouseCIMng.WarehouseCI>
    {
        private DataConverter converter = new DataConverter();
        private WarehouseCIMngEntities CreateContext()
        {
            return new WarehouseCIMngEntities(DALBase.Helper.CreateEntityConnectionString("WarehouseCIMngModel"));
        }

        public override IEnumerable<DTO.WarehouseCIMng.WarehouseCISearch> GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string InvoiceNo = string.Empty;
            string RefNo = string.Empty;
            string OrderNo = string.Empty;
            string ClientUD = string.Empty;
            string ClientNM = string.Empty;

            if (filters.ContainsKey("InvoiceNo")) InvoiceNo = filters["InvoiceNo"].ToString();
            if (filters.ContainsKey("RefNo")) RefNo = filters["RefNo"].ToString();
            if (filters.ContainsKey("OrderNo")) OrderNo = filters["OrderNo"].ToString();
            if (filters.ContainsKey("ClientUD")) ClientUD = filters["ClientUD"].ToString();
            if (filters.ContainsKey("ClientNM")) ClientNM = filters["ClientNM"].ToString();
            
            //try to get data
            try
            {
                using (WarehouseCIMngEntities context = CreateContext())
                {
                    totalRows = context.WarehouseCIMng_function_SearchWarehouseCI(orderBy,orderDirection,InvoiceNo,RefNo,OrderNo,ClientUD,ClientNM).Count();
                    var result = context.WarehouseCIMng_function_SearchWarehouseCI(orderBy, orderDirection, InvoiceNo, RefNo, OrderNo, ClientUD, ClientNM);
                    
                    return converter.DB2DTO_WarehouseCISearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Warning };
                return new List<DTO.WarehouseCIMng.WarehouseCISearch>();
            }
        }

        public override DTO.WarehouseCIMng.WarehouseCI GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (WarehouseCIMngEntities context = CreateContext())
                {
                    WarehouseCIMng_WarehouseCI_View dbItem;
                    dbItem = context.WarehouseCIMng_WarehouseCI_View
                        .Include("WarehouseCIMng_WarehouseCIDetail_View")
                        .Include("WarehouseCIMng_WarehouseCIExtDetail_View")
                        .FirstOrDefault(o => o.WarehouseCIID == id);
                    return converter.DB2DTO_WarehouseCI(dbItem);
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Warning };
                return new DTO.WarehouseCIMng.WarehouseCI();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseCIMngEntities context = CreateContext())
                {
                    WarehouseCI dbItem = context.WarehouseCI.FirstOrDefault(o => o.WarehouseCIID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Invoice not found!";
                        return false;
                    }
                    else
                    {
                        List<WarehouseCIDetail> need_delete_details = new List<WarehouseCIDetail>();
                        foreach (WarehouseCIDetail dbDetail in dbItem.WarehouseCIDetail)
                        {
                            need_delete_details.Add(dbDetail);
                        }

                        List<WarehouseCIExtDetail> need_delete_extdetail = new List<WarehouseCIExtDetail>();
                        foreach (WarehouseCIExtDetail dbExtDetail in dbItem.WarehouseCIExtDetail)
                        {
                            need_delete_extdetail.Add(dbExtDetail);
                        }

                        foreach (var item in need_delete_details)
                        {
                            context.WarehouseCIDetail.Remove(item);
                        }
                        foreach (var item in need_delete_extdetail)
                        {
                            context.WarehouseCIExtDetail.Remove(item);
                        }

                        context.WarehouseCI.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Warning };
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.WarehouseCIMng.WarehouseCI dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (WarehouseCIMngEntities context = CreateContext())
                {
                    WarehouseCI dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new WarehouseCI();
                        context.WarehouseCI.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.WarehouseCI.FirstOrDefault(o => o.WarehouseCIID == id);
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

                        converter.DTO2DB_WarehouseCI(dtoItem, ref dbItem);

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
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.WarehouseCIID, out notification);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.InnerException.InnerException.Message, Type = Library.DTO.NotificationType.Warning };
                return false;
            }
        }

        public DTO.WarehouseCIMng.DataContainer GetDataContainer(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DAL.Support.DataFactory factory = new Support.DataFactory();
            //try to get data
            try
            {
                using (WarehouseCIMngEntities context = CreateContext())
                {
                    DTO.WarehouseCIMng.DataContainer dtoItem = new DTO.WarehouseCIMng.DataContainer();

                    if (id > 0)
                    {
                        WarehouseCIMng_WarehouseCI_View dbItem;
                        dbItem = context.WarehouseCIMng_WarehouseCI_View
                                .Include("WarehouseCIMng_WarehouseCIDetail_View")
                                .Include("WarehouseCIMng_WarehouseCIExtDetail_View")
                                .FirstOrDefault(o => o.WarehouseCIID == id);
                        DTO.WarehouseCIMng.WarehouseCI WarehouseCIDTO = converter.DB2DTO_WarehouseCI(dbItem);
                        dtoItem.WarehouseCIData = WarehouseCIDTO;
                    }
                    else
                    {
                        dtoItem.WarehouseCIData = new DTO.WarehouseCIMng.WarehouseCI();
                        dtoItem.WarehouseCIData.WarehouseCIDetails = new List<DTO.WarehouseCIMng.WarehouseCIDetail>();
                        dtoItem.WarehouseCIData.WarehouseCIExtDetails = new List<DTO.WarehouseCIMng.WarehouseCIExtDetail>();
                    }

                    dtoItem.Clients = factory.GetClient().ToList();
                    dtoItem.Currency = factory.GetCurrency().ToList();
                    dtoItem.WareHouses = factory.GetWareHouse().ToList();
                    //dtoItem.TurnOverLedgerAccount = factory.T
                    return dtoItem;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Warning};
                return new DTO.WarehouseCIMng.DataContainer();
            }
        }
    }
}
