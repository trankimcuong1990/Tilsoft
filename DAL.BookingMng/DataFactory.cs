using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.OMSHelper;
using System.IO;
using DALBase;

namespace DAL.BookingMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.BookingMng.SearchFormData, DTO.BookingMng.EditFormData, DTO.BookingMng.Booking>
    {
        private DataConverter converter = new DataConverter();
        private Support.DataFactory supportFactory = new Support.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        private BookingMngEntities CreateContext()
        {
            return new BookingMngEntities(DALBase.Helper.CreateEntityConnectionString("BookingMngModel"));
        }

        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            _tempFolder = tempFolder + @"\";
        }

        public override DTO.BookingMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.BookingMng.SearchFormData data = new DTO.BookingMng.SearchFormData();
            data.Data = new List<DTO.BookingMng.BookingSearchResult>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            //try to get data
            try
            {
                using (BookingMngEntities context = CreateContext())
                {
                    string BookingUD = null;
                    string Season = null;
                    string BLNo = null;
                    string ContainerNo = null;
                    string ClientNM = null;

                    if (filters.ContainsKey("BookingUD") && !string.IsNullOrEmpty(filters["BookingUD"].ToString()))
                    {
                        BookingUD = filters["BookingUD"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
                    {
                        Season = filters["Season"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("BLNo") && !string.IsNullOrEmpty(filters["BLNo"].ToString()))
                    {
                        BLNo = filters["BLNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ContainerNo") && !string.IsNullOrEmpty(filters["ContainerNo"].ToString()))
                    {
                        ContainerNo = filters["ContainerNo"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientNM = filters["ClientUD"].ToString().Replace("'", "''");
                    }

                    totalRows = context.BookingMng_function_SearchBooking(BookingUD, Season, BLNo, ContainerNo, ClientNM, orderBy, orderDirection).Count();
                    var result = context.BookingMng_function_SearchBooking(BookingUD, Season, BLNo, ContainerNo, ClientNM, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_BookingSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            }
            return data;
        }

        public override DTO.BookingMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BookingMngEntities context = CreateContext())
                {
                    Booking dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Booking No: " + dbItem.BookingUD + " not found!");
                    }
                    else
                    {
                        if (!dbItem.IsConfirmed.HasValue || !dbItem.IsConfirmed.Value)
                        {
                            context.Booking.Remove(dbItem);
                            context.SaveChanges();
                            return true;
                        }
                        else
                        {
                            throw new Exception("Can not delete the approved booking");
                        }
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

        public override bool UpdateData(int id, ref DTO.BookingMng.Booking dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (BookingMngEntities context = CreateContext())
                {
                    Booking dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Booking();
                        dbItem.CreatedBy = dtoItem.UpdatedBy;
                        dbItem.CreatedDate = DateTime.Now;
                        context.Booking.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    }

                    if (dbItem == null)
                    {
                        throw new Exception("Booking not found!");
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // check if status = CONFIRMED
                        if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONFIRMED_CONFLICT);
                        }

                        //file processing
                        Library.FileHelper.FileManager fileMng = new Library.FileHelper.FileManager(FrameworkSetting.Setting.AbsoluteFileFolder);
                        string fileNeedDeleted = string.Empty;
                        string thumbnailFileNeedDeleted = string.Empty;

                        if (dtoItem.BLFileHasChange)
                        {
                            //check to delete file does not exist in database
                            if (!string.IsNullOrEmpty(dtoItem.BLFile))
                            {
                                fwFactory.GetDBFileLocation(dtoItem.BLFile, out fileNeedDeleted, out thumbnailFileNeedDeleted);
                                if (!string.IsNullOrEmpty(fileNeedDeleted))
                                {
                                    try
                                    {
                                        fileMng.DeleteFile(fileNeedDeleted);
                                    }
                                    catch { }
                                }
                            }
                            if (string.IsNullOrEmpty(dtoItem.NewBLFile))
                            {
                                // remove file registration in File table
                                fwFactory.RemoveFile(dtoItem.BLFile);
                                // reset file in table Contract
                                dtoItem.BLFile = string.Empty;
                            }
                            else
                            {
                                string outDBFileLocation = "";
                                string outFileFullPath = "";
                                string outFilePointer = "";
                                // copy new file
                                fileMng.StoreFile(_tempFolder +  dtoItem.NewBLFile, out outDBFileLocation, out outFileFullPath);

                                if (File.Exists(outFileFullPath))
                                {
                                    FileInfo info = new FileInfo(outFileFullPath);
                                    // insert/update file registration in database
                                    fwFactory.UpdateFile(dtoItem.BLFile, dtoItem.NewBLFile, outDBFileLocation, info.Extension, "", (int)info.Length, out outFilePointer);
                                    // set file database pointer
                                    dtoItem.BLFile = outFilePointer;
                                }
                            }
                        }
                        converter.DTO2DB(dtoItem, ref dbItem);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;

                        // remove orphans
                        context.BookingDetail.Local.Where(o => o.Booking == null).ToList().ForEach(o => context.BookingDetail.Remove(o));
                        context.SaveChanges();

                        // generate booking code
                        if (id == 0)
                        {
                            //dbItem.BookingUD = supportFactory.GetSupplier().FirstOrDefault(o => o.SupplierID == dbItem.SupplierID).SupplierUD + "/" + dtoItem.ClientUD + "/" + dtoItem.Season.Substring(5, 4);
                            //using (DbContextTransaction scope = context.Database.BeginTransaction())
                            //{
                            //    context.Database.ExecuteSqlCommand("SELECT TOP 1 * FROM Booking WITH (TABLOCKX, HOLDLOCK)");
                            //    try
                            //    {
                            //        dbItem.BookingUD = context.Booking.Count(o => o.SupplierID == dbItem.SupplierID && o.Season == dbItem.Season).ToString().AddPrefix("0", 4) + "/" + dbItem.BookingUD;
                            //        context.SaveChanges();
                            //    }
                            //    catch { }
                            //    finally
                            //    {
                            //        scope.Commit();
                            //    }
                            //}
                            context.BookingMng_function_UpdateBookingRef(dbItem.BookingID);
                        }
                        dtoItem = GetData(dbItem.BookingID, 0, 0, "", out notification).Data;

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

        public override bool Approve(int id, ref DTO.BookingMng.Booking dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                if (string.IsNullOrEmpty(dtoItem.ShipedDate))
                {
                    throw new Exception("You need fill-in shipped date before confirm");
                }
                
                // only update the current data if the status is different than CONFIRMED
                if (!dtoItem.IsConfirmed.HasValue || !dtoItem.IsConfirmed.Value)
                {
                    // update current data
                    if (!UpdateData(id, ref dtoItem, out notification))
                    {
                        throw new Exception(notification.Message);
                    }
                }
                else
                {
                    throw new Exception(DALBase.Helper.TEXT_CONFIRMED_CONFLICT);
                }

                using (BookingMngEntities context = CreateContext())
                {
                    Booking dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                    if (dbItem != null)
                    {
                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = dtoItem.UpdatedBy;
                        dbItem.ConfirmedDate = DateTime.Now;
                        context.SaveChanges();

                        //dtoItem = GetData(id, out notification).Data;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Booking No: " + dtoItem.BookingUD + " not found!");
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

        public override bool Reset(int id, ref DTO.BookingMng.Booking dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                if (dtoItem.IsConfirmed.HasValue && dtoItem.IsConfirmed.Value)
                {
                    using (BookingMngEntities context = CreateContext())
                    {
                        Booking dbItem = context.Booking.FirstOrDefault(o => o.BookingID == id);
                        if (dbItem != null)
                        {
                            dbItem.IsConfirmed = false;
                            dbItem.ConfirmedBy = null;
                            dbItem.ConfirmedDate = null;
                            dbItem.UpdatedBy = dtoItem.UpdatedBy;
                            dbItem.UpdatedDate = DateTime.Now;
                            context.SaveChanges();
                            //dtoItem = GetData(id, out notification).Data;

                            return true;
                        }
                        else
                        {
                            throw new Exception("Booking No: " + dtoItem.BookingUD + " not found!");
                        }
                    }
                }
                else
                {
                    throw new Exception("Booking No: " + dtoItem.BookingUD + " is not confirmed, no need to reset!");
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
        
        public DTO.BookingMng.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            DTO.BookingMng.SearchFilterData data = new DTO.BookingMng.SearchFilterData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            data.Seasons = new List<DTO.Support.Season>();

            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }
            
            return data;
        }

        public DTO.BookingMng.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            DTO.BookingMng.InitFormData data = new DTO.BookingMng.InitFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            data.Seasons = new List<DTO.Support.Season>();
            data.Clients = new List<DTO.Support.Client>();
            data.Suppliers = new List<DTO.Support.Supplier>();

            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.Clients = supportFactory.GetClient().ToList();
                data.Suppliers = supportFactory.GetSupplier().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public DTO.BookingMng.EditFormData GetData(int id, int clientID, int supplierID, string season, out Library.DTO.Notification notification)
        {
            DTO.BookingMng.EditFormData data = new DTO.BookingMng.EditFormData();
            data.Data = new DTO.BookingMng.Booking();
            data.Data.Details = new List<DTO.BookingMng.LoadingPlan>();
            data.DeliveryTerms = new List<DTO.Support.DeliveryTerm>();
            data.Forwarders = new List<DTO.Support.Forwarder>();
            data.MovementTerms = new List<DTO.Support.MovementTerm>();
            data.OceanFreights = new List<DTO.Support.StringCollectionItem>();
            data.PODs = new List<DTO.Support.POD>();
            data.POLs = new List<DTO.Support.POL>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (BookingMngEntities context = CreateContext())
                {
                    if (id == 0)
                    {
                        data.Data.ClientID = clientID;
                        data.Data.ClientUD = supportFactory.GetClient().FirstOrDefault(o => o.ClientID == clientID).ClientUD;
                        data.Data.SupplierID = supplierID;
                        data.Data.SupplierNM = supportFactory.GetSupplier().FirstOrDefault(o => o.SupplierID == supplierID).SupplierNM;
                        data.Data.Season = season;
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_Booking(context.BookingMng_Booking_View.Include("BookingMng_LoadingPlan_View").FirstOrDefault(o => o.BookingID == id));
                        data.Data.ConcurrencyFlag_String = Convert.ToBase64String(data.Data.ConcurrencyFlag);
                    }
                }

                data.PODs = supportFactory.GetPOD().ToList();
                data.POLs = supportFactory.GetPOL().ToList();
                data.Forwarders = supportFactory.GetForwarder().ToList();
                data.DeliveryTerms = supportFactory.GetDeliveryTerm().ToList();
                data.MovementTerms = supportFactory.GetMovementTerm().ToList();
                data.OceanFreights = supportFactory.GetOceanFreight().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
            }

            return data;
        }

        public bool ConfirmETA(int bookingID, int confirmedBy, string ETA, int ETAType,out string concurrencyFlag_String, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = (ETAType == 1 ? "ETA 1 has been confirmed success" : "ETA 2 has been confirmed success") };
            concurrencyFlag_String = "";
            try
            {
                using (BookingMngEntities context = CreateContext())
                {
                    DateTime? eta = ETA.ConvertStringToDateTime();
                    context.BookingMng_function_ConfirmETA(bookingID, confirmedBy, eta, ETAType);
                    concurrencyFlag_String = Convert.ToBase64String(context.Booking.Where(o => o.BookingID == bookingID).FirstOrDefault().ConcurrencyFlag);
                }
                return true;
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
    }
}
