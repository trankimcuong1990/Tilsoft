using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryLoadingPlanMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryLoadingPlanMngEntities CreateContext()
        {
            return new FactoryLoadingPlanMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryLoadingPlanMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.LoadingPlanSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    string Season = null;
                    int? FactoryID = null;
                    int UserID = -1;
                    string ClientUD = null;
                    string BookingUD = null;
                    string BLNo = null;
                    string LoadingPlanUD = null;
                    string ContainerNo = null;
                    string SealNo = null;
                    string ArticleCode = null;
                    string Description = null;
                    string FactoryOrderUD = null;
                    if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                    {
                        FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                    }
                    if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                    {
                        UserID = Convert.ToInt32(filters["UserID"].ToString());
                    }
                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("BookingUD") && !string.IsNullOrEmpty(filters["BookingUD"].ToString()))
                    {
                        BookingUD = filters["BookingUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("BLNo") && !string.IsNullOrEmpty(filters["BLNo"].ToString()))
                    {
                        BLNo = filters["BLNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("LoadingPlanUD") && !string.IsNullOrEmpty(filters["LoadingPlanUD"].ToString()))
                    {
                        LoadingPlanUD = filters["LoadingPlanUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ContainerNo") && !string.IsNullOrEmpty(filters["ContainerNo"].ToString()))
                    {
                        ContainerNo = filters["ContainerNo"].ToString().Replace("'", "''");
                    }                    
                    if (filters.ContainsKey("SealNo") && !string.IsNullOrEmpty(filters["SealNo"].ToString()))
                    {
                        SealNo = filters["SealNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        Description = filters["Description"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FactoryOrderUD") && !string.IsNullOrEmpty(filters["FactoryOrderUD"].ToString()))
                    {
                        FactoryOrderUD = filters["FactoryOrderUD"].ToString().Replace("'", "''");
                    }
                    totalRows = context.FactoryLoadingPlanMng_function_SearchLoadingPlan(Season, FactoryID, UserID, ClientUD, BookingUD, BLNo, LoadingPlanUD, ContainerNo, SealNo, ArticleCode, Description, FactoryOrderUD, orderBy, orderDirection).Count();
                    var result = context.FactoryLoadingPlanMng_function_SearchLoadingPlan(Season, FactoryID, UserID, ClientUD, BookingUD, BLNo, LoadingPlanUD, ContainerNo, SealNo, ArticleCode, Description, FactoryOrderUD, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_LoadingPlanSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public Task<DTO.SearchFormData> GetDataWithFilterAsync(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            var tcs = new TaskCompletionSource<DTO.SearchFormData>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.LoadingPlanSearchResult>();
            totalRows = 0;

            try
            {
                string Season = null;
                int? FactoryID = null;
                int UserID = -1;
                string ClientUD = null;
                string BookingUD = null;
                string BLNo = null;
                string LoadingPlanUD = null;
                string ContainerNo = null;
                string SealNo = null;
                string ArticleCode = null;
                string Description = null;
                string FactoryOrderUD = null;
                if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
                {
                    Season = filters["Season"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryID") && !string.IsNullOrEmpty(filters["FactoryID"].ToString()))
                {
                    FactoryID = Convert.ToInt32(filters["FactoryID"].ToString());
                }
                if (filters.ContainsKey("UserID") && !string.IsNullOrEmpty(filters["UserID"].ToString()))
                {
                    UserID = Convert.ToInt32(filters["UserID"].ToString());
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("BookingUD") && !string.IsNullOrEmpty(filters["BookingUD"].ToString()))
                {
                    BookingUD = filters["BookingUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("BLNo") && !string.IsNullOrEmpty(filters["BLNo"].ToString()))
                {
                    BLNo = filters["BLNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("LoadingPlanUD") && !string.IsNullOrEmpty(filters["LoadingPlanUD"].ToString()))
                {
                    LoadingPlanUD = filters["LoadingPlanUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ContainerNo") && !string.IsNullOrEmpty(filters["ContainerNo"].ToString()))
                {
                    ContainerNo = filters["ContainerNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("SealNo") && !string.IsNullOrEmpty(filters["SealNo"].ToString()))
                {
                    SealNo = filters["SealNo"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                {
                    ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                {
                    Description = filters["Description"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FactoryOrderUD") && !string.IsNullOrEmpty(filters["FactoryOrderUD"].ToString()))
                {
                    FactoryOrderUD = filters["FactoryOrderUD"].ToString().Replace("'", "''");
                }

                int totalReturnRows = 0;
                Task task1 = Task.Factory.StartNew(() => {
                    using (FactoryLoadingPlanMngEntities context1 = CreateContext())
                    {
                        totalReturnRows = context1.FactoryLoadingPlanMng_function_SearchLoadingPlan(Season, FactoryID, UserID, ClientUD, BookingUD, BLNo, LoadingPlanUD, ContainerNo, SealNo, ArticleCode, Description, FactoryOrderUD, orderBy, orderDirection).Count();
                    }
                });
                Task task2 = Task.Factory.StartNew(() => {
                    using (FactoryLoadingPlanMngEntities context2 = CreateContext())
                    {
                        var result = context2.FactoryLoadingPlanMng_function_SearchLoadingPlan(Season, FactoryID, UserID, ClientUD, BookingUD, BLNo, LoadingPlanUD, ContainerNo, SealNo, ArticleCode, Description, FactoryOrderUD, orderBy, orderDirection);
                        data.Data = converter.DB2DTO_LoadingPlanSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    }
                });
                Task.WaitAll(task1, task2);
                totalRows = totalReturnRows;
                tcs.SetResult(data);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                tcs.SetException(ex);
            }
            return tcs.Task;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.LoadingPlan dtoLoadingPlan = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.LoadingPlan>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (fwFactory.CheckBookingPermission(userId, dtoLoadingPlan.BookingID.Value) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected booking data");
                }
                if (id > 0 && fwFactory.CheckLoadingPlanPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected loading plan data");
                }

                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    LoadingPlan dbItem = null;
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM LoadingPlan WITH (TABLOCKX, HOLDLOCK); SELECT * FROM LoadingPlanDetail WITH (TABLOCKX, HOLDLOCK); SELECT * FROM LoadingPlanSparepartDetail WITH (TABLOCKX, HOLDLOCK);");

                        try
                        {
                            if (id == 0)
                            {
                                dbItem = new LoadingPlan();
                                dbItem.LoadingPlanUD = context.FactoryLoadingPlanMng_function_GenerateLoadingPlanCode(dtoLoadingPlan.FactoryID).FirstOrDefault();
                                context.LoadingPlan.Add(dbItem);
                            }
                            else
                            {
                                dbItem = context.LoadingPlan.FirstOrDefault(o => o.LoadingPlanID == id);
                            }

                            if (dbItem == null)
                            {
                                notification.Message = "Loading plan not found!";
                                return false;
                            }
                            else
                            {
                                // check concurrency
                                if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoLoadingPlan.ConcurrencyFlag)))
                                {
                                    throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                                }

                                // check if invoice already issued
                                PurchasingInvoice dbInvoice = context.PurchasingInvoice.FirstOrDefault(o => o.BookingID == dtoLoadingPlan.BookingID);
                                if (dbInvoice == null)
                                {
                                    // check remain quantity
                                    foreach (DTO.LoadingPlanDetail dtoDetail in dtoLoadingPlan.LoadingPlanDetails)
                                    {
                                        List<FactoryLoadingPlanMng_FactoryOrderDetailLoaded_View> dbQnts = context.FactoryLoadingPlanMng_FactoryOrderDetailLoaded_View.Where(o => o.FactoryOrderDetailID == dtoDetail.FactoryOrderDetailID && o.LoadingPlanID != dtoLoadingPlan.LoadingPlanID).ToList();
                                        if (dtoDetail.Quantity.HasValue)
                                        {
                                            if (dtoDetail.OrderQnt.Value < dbQnts.Sum(o => o.Quantity) + dtoDetail.Quantity.Value)
                                            {
                                                string OldLoadingPlan = string.Empty;
                                                dbQnts.ForEach(o => OldLoadingPlan += o.LoadingPlanUD + ";");
                                                throw new Exception("Quantity exceed ordered quantity<br/><u>Product</u>: " + dtoDetail.Description + "<br/><u>Total ordered</u>: " + dtoDetail.OrderQnt.Value.ToString() + "<br/><u>Total loaded</u>: " + dbQnts.Sum(o => o.Quantity).ToString() + "<br/><u>Previous loading plan</u>: " + OldLoadingPlan + "<br/><u>To be loaded in this loading plan</u>: " + dtoDetail.Quantity);
                                            }
                                        }
                                    }

                                    foreach (DTO.LoadingPlanSparepartDetail dtoSparepartDetail in dtoLoadingPlan.LoadingPlanSparepartDetails)
                                    {
                                        List<FactoryLoadingPlanMng_FactoryOrderSparepartDetailLoaded_View> dbQnts = context.FactoryLoadingPlanMng_FactoryOrderSparepartDetailLoaded_View.Where(o => o.FactoryOrderSparepartDetailID == dtoSparepartDetail.FactoryOrderSparepartDetailID && o.LoadingPlanID != dtoLoadingPlan.LoadingPlanID).ToList();
                                        if (dtoSparepartDetail.Quantity.HasValue)
                                        {
                                            if (dtoSparepartDetail.OrderQnt < dbQnts.Sum(o => o.Quantity) + dtoSparepartDetail.Quantity.Value)
                                            {
                                                string OldLoadingPlan = string.Empty;
                                                dbQnts.ForEach(o => OldLoadingPlan += o.LoadingPlanUD + ";");
                                                throw new Exception("Quantity exceed ordered quantity<br/><u>Sparepart</u>: " + dtoSparepartDetail.Description + "<br/><u>Total ordered</u>: " + dtoSparepartDetail.OrderQnt.Value.ToString() + "<br/><u>Total loaded</u>: " + dbQnts.Sum(o => o.Quantity).ToString() + "<br/><u>Previous loading plan</u>: " + OldLoadingPlan + "<br/><u>To be loaded in this loading plan</u>: " + dtoSparepartDetail.Quantity);
                                            }
                                        }                                        
                                    }

                                    converter.DTO2DB(dtoLoadingPlan, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", true);
                                    context.LoadingPlanDetail.Local.Where(o => o.LoadingPlan == null).ToList().ForEach(o => context.LoadingPlanDetail.Remove(o));
                                    context.LoadingPlanSparepartDetail.Local.Where(o => o.LoadingPlan == null).ToList().ForEach(o => context.LoadingPlanSparepartDetail.Remove(o));
                                }
                                else
                                {
                                    converter.DTO2DB(dtoLoadingPlan, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", false);
                                }

                                dbItem.UpdatedBy = userId;
                                dbItem.UpdatedDate = DateTime.Now;

                                // remove orphan
                                context.LoadingPlanDetail.Local.Where(o => o.LoadingPlan == null).ToList().ForEach(o => context.LoadingPlanDetail.Remove(o));
                                context.LoadingPlanSparepartDetail.Local.Where(o => o.LoadingPlan == null).ToList().ForEach(o => context.LoadingPlanSparepartDetail.Remove(o));

                                context.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }
                    }

                    dtoItem = GetData(userId, dbItem.LoadingPlanID, -1, -1, -1, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }


        //
        // CUSTOM FUNCTION
        //
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();
            data.Factories = new List<Support.DTO.Factory>();

            try
            {
                data.Seasons = supportFactory.GetSeason();
                data.Factories = supportFactory.GetFactory(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Factories = new List<Support.DTO.Factory>();

            try
            {
                data.Factories = supportFactory.GetFactory(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.BookingSearchResult> QuickSearchBooking(int userId, int factoryid, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.BookingSearchResult> data = new List<DTO.BookingSearchResult>();

            //try to get data
            try
            {
                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_BookingSearchResultList(context.FactoryLoadingPlanMng_function_SearchBooking(userId, factoryid, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.LoadingPlan> QuickSearchParentLoadingPlan(string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.LoadingPlan> data = new List<DTO.LoadingPlan>();

            //try to get data
            try
            {
                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    DTO.LoadingPlan dtoItem = converter.DB2DTO_LoadingPlan(context.FactoryLoadingPlanMng_function_SearchParentLoadingPlan(query).ToList().FirstOrDefault());
                    if (dtoItem != null)
                    {
                        data.Add(dtoItem);
                    }                    
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int bookingID, int factoryID, int parentID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.LoadingPlan();
            data.Data.LoadingPlanDetails = new List<DTO.LoadingPlanDetail>();
            data.Data.LoadingPlanSparepartDetails = new List<DTO.LoadingPlanSparepartDetail>();
            data.ContainerTypes = new List<Support.DTO.ContainerType>();

            //try to get data
            try
            {
                // check permission
                if (id > 0)
                {
                    if (fwFactory.CheckLoadingPlanPermission(userId, id) == 0)
                    {
                        throw new Exception("Current user don't have access permission for the selected loading plan data");
                    }
                }
                else
                {
                    if (bookingID > 0)
                    {
                        if (fwFactory.CheckBookingPermission(userId, bookingID) == 0)
                        {
                            throw new Exception("Current user don't have access permission for the selected booking data");
                        }
                    }                    
                }

                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    if (id == 0)
                    {
                        if (bookingID <= 0 && parentID <= 0)
                        {
                            throw new Exception("Booking/Parent loading plan is invalid!");
                        }

                        // get booking info
                        if (bookingID > 0)
                        {
                            FactoryLoadingPlanMng_BookingSearchResult_View dbBooking = context.FactoryLoadingPlanMng_BookingSearchResult_View.FirstOrDefault(o => o.BookingID == bookingID);
                            if (dbBooking != null)
                            {
                                DTO.BookingSearchResult dtoBooking = converter.DB2DTO_BookingSearchResult(dbBooking);
                                data.Data.BookingID = bookingID;
                                data.Data.BookingUD = dtoBooking.BookingUD;
                                data.Data.BLNo = dtoBooking.BLNo;
                                data.Data.Season = dtoBooking.Season;
                                data.Data.CutOffDate = dtoBooking.CutOffDate;
                                data.Data.Feeder = dtoBooking.Feeder;
                                data.Data.Vessel = dtoBooking.Vessel;
                                data.Data.ETA = dtoBooking.ETA;
                                data.Data.ETD = dtoBooking.ETD;
                                data.Data.ClientUD = dtoBooking.ClientUD;
                                data.Data.ForwarderNM = dtoBooking.ForwarderNM;
                            }
                            else
                            {
                                throw new Exception("Booking not exists!");
                            }
                        }

                        // get parent loading plan info
                        if (parentID > 0)
                        {
                            FactoryLoadingPlanMng_LoadingPlan_View dbLoadingPlan = context.FactoryLoadingPlanMng_LoadingPlan_View.FirstOrDefault(o => o.LoadingPlanID == parentID);
                            if (dbLoadingPlan != null)
                            {
                                if (dbLoadingPlan.ParentID.HasValue && dbLoadingPlan.ParentID.Value > 1)
                                {
                                    throw new Exception("Invalid parent loading plan!");
                                }
                                data.Data.BookingID = dbLoadingPlan.BookingID;
                                data.Data.BookingUD = dbLoadingPlan.BookingUD;
                                data.Data.BLNo = dbLoadingPlan.BLNo;
                                data.Data.Season = dbLoadingPlan.Season;
                                data.Data.CutOffDate = dbLoadingPlan.CutOffDate.HasValue ? dbLoadingPlan.CutOffDate.Value.ToString("dd/MM/yyyy") : "";
                                data.Data.Feeder = dbLoadingPlan.Feeder;
                                data.Data.Vessel = dbLoadingPlan.Vessel;
                                data.Data.ETA = dbLoadingPlan.ETA.HasValue ? dbLoadingPlan.ETA.Value.ToString("dd/MM/yyyy") : "";
                                data.Data.ETD = dbLoadingPlan.ETD.HasValue ? dbLoadingPlan.ETD.Value.ToString("dd/MM/yyyy") : "";
                                data.Data.ClientUD = dbLoadingPlan.ClientUD;
                                data.Data.ForwarderNM = dbLoadingPlan.ForwarderNM;

                                data.Data.ContainerNo = dbLoadingPlan.ContainerNo;
                                data.Data.ContainerTypeID = dbLoadingPlan.ContainerTypeID;
                                data.Data.SealNo = dbLoadingPlan.SealNo;
                                data.Data.ParentContainerNo = dbLoadingPlan.ContainerNo;
                                data.Data.ParentContainerTypeNM = dbLoadingPlan.ContainerTypeNM;
                                data.Data.ParentLoadingPlanUD = dbLoadingPlan.LoadingPlanUD;
                                data.Data.ParentSealNo = dbLoadingPlan.SealNo;

                                data.Data.ParentID = parentID;
                            }
                            else
                            {
                                throw new Exception("Parent loading plan not exists!");
                            }
                        }
                        
                        data.Data.FactoryID = factoryID;

                        // get factory info
                        Module.Support.DTO.Factory dtoFactory = supportFactory.GetFactory(userId).FirstOrDefault(o => o.FactoryID == factoryID);
                        if (dtoFactory != null)
                        {
                            data.Data.FactoryUD = dtoFactory.FactoryUD;
                        }
                        else
                        {
                            throw new Exception("Factory not exists!");
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_LoadingPlan(context.FactoryLoadingPlanMng_LoadingPlan_View
                            .Include("FactoryLoadingPlanMng_LoadingPlanDetail_View")
                            .Include("FactoryLoadingPlanMng_LoadingPlanSparepartDetail_View")
                            .FirstOrDefault(o => o.LoadingPlanID == id));
                    }

                    data.ContainerTypes = supportFactory.GetContainerType();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.FactoryOrderDetailSearchResult> QuickSearchFactoryOrderDetail(int userId, int factoryid, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderDetailSearchResult> data = new List<DTO.FactoryOrderDetailSearchResult>();

            //try to get data
            try
            {
                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryOrderDetailSearchResultList(context.FactoryLoadingPlanMng_function_SearchFactoryOrderDetail(userId, factoryid, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.FactoryOrderSparepartDetailSearchResult> QuickSearchFactoryOrderSparepartDetail(int userId, int factoryid, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderSparepartDetailSearchResult> data = new List<DTO.FactoryOrderSparepartDetailSearchResult>();

            //try to get data
            try
            {
                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_FactoryOrderSparepartDetailSearchResultList(context.FactoryLoadingPlanMng_function_SearchFactoryOrderSparepartDetail(userId, factoryid, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool DeleteData(int userId, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckLoadingPlanPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected loading plan data");
                }

                using (FactoryLoadingPlanMngEntities context = CreateContext())
                {
                    LoadingPlan dbItem = context.LoadingPlan.FirstOrDefault(o => o.LoadingPlanID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Loading plan not found!");
                    }

                    // check if loading plan already confirmed
                    if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                    {
                        throw new Exception("Can not delete the confirmed loading plan!");
                    }

                    // check if loading plan already has invoice
                    PurchasingInvoice dbInvoice = context.PurchasingInvoice.FirstOrDefault(o => o.BookingID == dbItem.BookingID);
                    if (dbInvoice != null)
                    {
                        throw new Exception("Can not delete the loading plan which already has invoice issued!");
                    }

                    // everything ok, delete the loading plan
                    if (dbItem.ProductPicture1 != string.Empty) fwFactory.RemoveImageFile(dbItem.ProductPicture1);
                    if (dbItem.ProductPicture2 != string.Empty) fwFactory.RemoveImageFile(dbItem.ProductPicture2);
                    if (dbItem.ContainerPicture1 != string.Empty) fwFactory.RemoveImageFile(dbItem.ContainerPicture1);
                    if (dbItem.ContainerPicture2 != string.Empty) fwFactory.RemoveImageFile(dbItem.ContainerPicture2);
                    context.LoadingPlan.Remove(dbItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }
    }
}
