using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Module.FactoryPLCMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryPLCMngEntities CreateContext()
        {
            return new FactoryPLCMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryPLCMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.PLCSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FactoryPLCMngEntities context = CreateContext())
                {
                    string Season = null;
                    int? FactoryID = null;
                    int UserID = -1;
                    string ArticleCode = null;
                    string Description = null;
                    string ClientUD = null;
                    string BookingUD = null;
                    string BLNo = null;
                    string ProformaInvoiceNo = null;
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
                    if (filters.ContainsKey("ArticleCode") && !string.IsNullOrEmpty(filters["ArticleCode"].ToString()))
                    {
                        ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        Description = filters["Description"].ToString().Replace("'", "''");
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
                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }
                    totalRows = context.FactoryPLCMng_function_SearchPLC(Season, FactoryID, UserID, ArticleCode, Description, ClientUD, BookingUD, BLNo, ProformaInvoiceNo, orderBy, orderDirection).Count();
                    var result = context.FactoryPLCMng_function_SearchPLC(Season, FactoryID, UserID, ArticleCode, Description, ClientUD, BookingUD, BLNo, ProformaInvoiceNo, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_PLCSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    // fill in the proformainvoice value
                    string inputParam = string.Join(",", data.Data.Select(o => o.PLCID).ToArray());
                    foreach (FactoryPLCMng_ProformaInvoice_View proformaInvoice in context.FactoryPLCMng_function_getPLCProformaInvoiceNo(inputParam).ToList())
                    {
                        if (!string.IsNullOrEmpty(proformaInvoice.ProformaInvoiceNo))
                        {
                            data.Data.FirstOrDefault(o => o.PLCID == proformaInvoice.PLCID).ProformaInvoiceNo = proformaInvoice.ProformaInvoiceNo;
                        }
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
            DTO.PLC dtoPLC = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PLC>();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryPLCMngEntities context = CreateContext())
                {
                    // check if user can update this information 
                    int? permissionResult = context.FactoryPLCMng_function_CheckUserPermission(userId, dtoPLC.OfferLineID, dtoPLC.OfferLineSparepartID, dtoPLC.BookingID).FirstOrDefault();
                    if (!permissionResult.HasValue && permissionResult.Value == 0)
                    {
                        throw new Exception("Current user dont have permission to edit this data (data permission validation failed)");
                    }
                    var checkImg = dtoPLC.PLCImages.FirstOrDefault(o => o.ImageTypeID == 15);
                    if(checkImg == null)
                    {
                        throw new Exception("Please add Overral Image !!!");
                    }
                    PLC dbItem = null;
                    if (id == 0)
                    {
                        if (!dtoPLC.BookingID.HasValue || !dtoPLC.FactoryID.HasValue)
                        {
                            throw new Exception("You have to select Booking and Factory before create PLC");
                        }
                        int bookingID = dtoPLC.BookingID.Value;
                        int factoryID = dtoPLC.FactoryID.Value;
                        if (dtoPLC.OfferLineID.HasValue)
                        {
                            int offerLineID = dtoPLC.OfferLineID.Value;
                            if (context.PLC.FirstOrDefault(o => o.OfferLineID == offerLineID && o.BookingID == bookingID && o.FactoryID == factoryID) != null)
                            {
                                throw new Exception("Item already has PLC");
                            }
                        }
                        else if(dtoPLC.OfferLineSparepartID.HasValue)
                        {
                            int offerLineSparepartID = dtoPLC.OfferLineSparepartID.Value;
                            if (context.PLC.FirstOrDefault(o => o.OfferLineSparepartID == offerLineSparepartID && o.BookingID == bookingID && o.FactoryID == factoryID) != null)
                            {
                                throw new Exception("Item already has PLC");
                            }
                        }
                        else if (dtoPLC.OfferLineSampleProductID.HasValue)
                        {
                            int offerLineSampleProductID = dtoPLC.OfferLineSampleProductID.Value;
                            if (context.PLC.FirstOrDefault(o => o.OfferLineSampleProductID == offerLineSampleProductID && o.BookingID == bookingID && o.FactoryID == factoryID) != null)
                            {
                                throw new Exception("Item already has PLC");
                            }
                        }

                        dbItem = new PLC();
                        context.PLC.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PLC.FirstOrDefault(o => o.PLCID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "PLC not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoPLC.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB(dtoPLC, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\");
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphan
                        context.PLCImage.Local.Where(o => o.PLC == null).ToList().ForEach(o => context.PLCImage.Remove(o));

                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.PLCID, -1, -1, -1, -1,-1, out notification).Data;
                        return true;
                    }
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
                using (FactoryPLCMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_BookingSearchResultList(context.FactoryPLCMng_function_SearchBooking(userId, factoryid, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.ItemSearchResult> QuickSearchItem(int userId, int factoryid, int bookingid, string query, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ItemSearchResult> data = new List<DTO.ItemSearchResult>();

            //try to get data
            try
            {
                using (FactoryPLCMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ItemSearchResultList(context.FactoryPLCMng_function_SearchItem(userId, factoryid, bookingid, query).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetData(int userId, int id, int bookingID, int factoryID, int offerLineID, int offerLineSampleProductID, int offerLineSparepartID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.PLC();
            data.Data.PLCImages = new List<DTO.PLCImage>();
            data.PLCImageTypes = new List<Support.DTO.PLCImageType>();

            //try to get data
            try
            {
                // check permission
                if (id > 0 && fwFactory.CheckPLCPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected plc data");
                }

                using (FactoryPLCMngEntities context = CreateContext())
                {
                    if (id == 0)
                    {
                        // get item info
                        FactoryPLCMng_ItemForCreatePLC_View dbInfo = null;
                        if (offerLineID > 0)
                        {
                            dbInfo = context.FactoryPLCMng_function_GetItemInfo(userId, factoryID, offerLineID, null, null).FirstOrDefault();
                        }
                        else if(offerLineSparepartID > 0)
                        {
                            dbInfo = context.FactoryPLCMng_function_GetItemInfo(userId, factoryID, null, offerLineSparepartID, null).FirstOrDefault();
                        }
                        else if (offerLineSampleProductID > 0)
                        {
                            dbInfo = context.FactoryPLCMng_function_GetItemInfo(userId, factoryID, null, null, offerLineSampleProductID).FirstOrDefault();
                        }
                        if (dbInfo != null)
                        {
                            data.Data.FactoryID = factoryID;
                            if (offerLineID > 0)
                            {
                                data.Data.OfferLineID = offerLineID;
                            }
                            else if(offerLineSparepartID > 0)
                            {
                                data.Data.OfferLineSparepartID = offerLineSparepartID;
                            }
                            else if (offerLineSampleProductID > 0)
                            {
                                data.Data.OfferLineSampleProductID = offerLineSampleProductID;
                            }
                            data.Data.ArticleCode = dbInfo.ArticleCode;
                            data.Data.Description = dbInfo.Description;
                            data.Data.FactoryOrderUD = dbInfo.FactoryOrderUD;
                            data.Data.ClientUD = dbInfo.ClientUD;
                            data.Data.FactoryUD = dbInfo.FactoryUD;

                            //packaging info
                            if (offerLineID > 0) {
                                var dbPackaging = context.FactoryPLCMng_Packaging_View.Where(o => o.OfferLineID == offerLineID).FirstOrDefault();
                                if (dbPackaging != null)
                                {
                                    data.Data.PackagingMethodNM = dbPackaging.PackagingMethodNM;
                                    data.Data.NetWeight = dbPackaging.NetWeight;
                                    data.Data.GrossWeight = dbPackaging.GrossWeight;
                                    data.Data.CBMS = dbPackaging.CBM;
                                    data.Data.PKGS = dbPackaging.CTNS;
                                    data.Data.HSCode = dbPackaging.HSCode;
                                }
                            }
                            if (offerLineSampleProductID > 0) {
                                var dbPackaging = context.FactoryPLCMng_SampleProductPackaging_View.Where(o => o.OfferLineSampleProductID == offerLineSampleProductID).FirstOrDefault();
                                if (dbPackaging != null)
                                {
                                    data.Data.PackagingMethodNM = dbPackaging.PackagingMethodNM;
                                    data.Data.NetWeight = dbPackaging.NetWeight;
                                    data.Data.GrossWeight = dbPackaging.GrossWeight;
                                    data.Data.CBMS = dbPackaging.CBM;
                                    data.Data.PKGS = dbPackaging.CTNS;
                                    data.Data.HSCode = dbPackaging.HSCode;
                                }
                            }
                                                                                   
                        }
                        else
                        {
                            throw new Exception("Item not exists!");
                        }

                        // get booking info
                        FactoryPLCMng_BookingSearchResult_View bookingInfo = context.FactoryPLCMng_function_GetBookingInfo(userId, factoryID, bookingID).FirstOrDefault();
                        if (bookingInfo != null)
                        {
                            data.Data.BookingID = bookingID;
                            data.Data.BookingUD = bookingInfo.BookingUD;
                            data.Data.BLNo = bookingInfo.BLNo;
                            data.Data.ContainerNo = "";
                            data.Data.LoadingPlanUD = "";
                            data.Data.Season = bookingInfo.Season;
                        }
                        else
                        {
                            throw new Exception("Booking not exists!");
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_PLC(context.FactoryPLCMng_PLC_View.Include("FactoryPLCMng_PLCImage_View").FirstOrDefault(o => o.PLCID == id));
                    }

                    data.PLCImageTypes = supportFactory.GetPLCImageType();
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
                if (id > 0 && fwFactory.CheckPLCPermission(userId, id) == 0)
                {
                    throw new Exception("Current user don't have access permission for the selected plc data");
                }

                using (FactoryPLCMngEntities context = CreateContext())
                {
                    // check if can delete PLC
                    string loadingPlanUD = context.FactoryPLCMng_function_CheckIfCanDeletePLC(id).FirstOrDefault();
                    if (!string.IsNullOrEmpty(loadingPlanUD))
                    {
                        throw new Exception("Current PLC already exists in loading plan: " + loadingPlanUD);
                    }

                    // everything ok, delete the plc
                    PLC dbItem = context.PLC.FirstOrDefault(o => o.PLCID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("PLC not found");
                    }

                    // remove image
                    foreach (PLCImage dbImage in dbItem.PLCImage)
                    {
                        if (!string.IsNullOrEmpty(dbImage.ImageFile))
                        {
                            fwFactory.RemoveImageFile(dbImage.ImageFile);
                        }
                    }
                    context.PLC.Remove(dbItem);
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

        public string GetReportData(int userId, string PLCIDs, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("FactoryPLCMng_function_getReportData", new SqlConnection(FrameworkSetting.Setting.SqlConnection));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PLCIDList", PLCIDs);
                adap.SelectCommand.Parameters.AddWithValue("@UserID", userId);
                adap.TableMappings.Add("Table", "PLCManagement2_PLCReport_View");
                adap.TableMappings.Add("Table1", "PLCManagement2_PLCImageReport_View");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "PLC");
                //return string.Empty;

                // generate xml file
                //return Library.Helper.CreateCOMReportFileImportDataOnly2(ds, "PLC");
                //return Library.Helper.CreateReportFiles(ds, "PLC");
                return Library.Helper.CreateReportFileWithEPPlus(ds, "PLC");
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
