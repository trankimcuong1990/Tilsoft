using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL.PLCMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.PLCMng.SearchFormData, DTO.PLCMng.EditFormData, DTO.PLCMng.PLC>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private PLCMngEntities CreateContext()
        {
            return new PLCMngEntities(DALBase.Helper.CreateEntityConnectionString("PLCMngModel"));
        }

        public override DTO.PLCMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PLCMng.SearchFormData data = new DTO.PLCMng.SearchFormData();
            data.Data = new List<DTO.PLCMng.PLCSearchResult>();
            totalRows = 0;

            string ArticleCode = null;
            string Description = null;
            string FactoryUD = null;
            string BookingUD = null;
            string BLNo = null;
            string Season = null;
            string ClientUD = null;
            string ContainerNo = null;
            string LoadingPlanUD = null;
            string ProformaInvoiceNo = null;
            string IsConfirmed = null;

            if (filters.ContainsKey("ArticleCode"))
            {
                ArticleCode = filters["ArticleCode"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Description"))
            {
                Description = filters["Description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD"))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("BookingUD"))
            {
                BookingUD = filters["BookingUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("BLNo"))
            {
                BLNo = filters["BLNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD"))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ContainerNo"))
            {
                ContainerNo = filters["ContainerNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("LoadingPlanUD"))
            {
                LoadingPlanUD = filters["LoadingPlanUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo"))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsConfirmed") && !string.IsNullOrEmpty(filters["IsConfirmed"].ToString()))
            {
                IsConfirmed = filters["IsConfirmed"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (PLCMngEntities context = CreateContext())
                {
                    totalRows = context.PLCMng_function_SearchPLC(ArticleCode, Description, FactoryUD, BookingUD, BLNo, Season, ClientUD, ContainerNo, LoadingPlanUD, ProformaInvoiceNo, IsConfirmed, orderBy, orderDirection).Count();
                    var result = context.PLCMng_function_SearchPLC(ArticleCode, Description, FactoryUD, BookingUD, BLNo, Season, ClientUD, ContainerNo, LoadingPlanUD, ProformaInvoiceNo, IsConfirmed, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_PLCSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.PLCMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PLCMngEntities context = CreateContext())
                {
                    PLC dbItem = context.PLC.FirstOrDefault(o => o.PLCID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "PLC not found!";
                        return false;
                    }
                    else
                    {
                        Module.Framework.DAL.DataFactory frameworkFactory = new Module.Framework.DAL.DataFactory();
                        foreach (PLCImage dbImage in dbItem.PLCImage)
                        {
                            if (!string.IsNullOrEmpty(dbImage.ImageFile))
                            {
                                // remove image
                                frameworkFactory.RemoveImageFile(dbImage.ImageFile);
                            }
                        }

                        context.PLC.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.PLCMng.PLC dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PLCMngEntities context = CreateContext())
                {
                    PLC dbItem = null;
                    if (id == 0)
                    {
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
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB(dtoItem, this._TempFolder, ref dbItem);
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;
                        if (dtoItem.isRated)
                        {
                            dbItem.RatedBy = dtoItem.UpdatedBy;
                            dbItem.RatedDate = DateTime.Now;
                        }
                        context.PLCImage.Local.Where(o => o.PLC == null).ToList().ForEach(o => context.PLCImage.Remove(o));
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.PLCID, -1, -1, -1, -1, out notification).Data;
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

        public override bool Approve(int id, ref DTO.PLCMng.PLC dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PLCMngEntities context = CreateContext())
                {
                    PLC dbItem = context.PLC.FirstOrDefault(o => o.PLCID == id);

                    dbItem.IsConfirmed = true;
                    dbItem.ConfirmedDate = DateTime.Now;
                    dbItem.ConfirmedBy = dtoItem.UpdatedBy;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Reset(int id, ref DTO.PLCMng.PLC dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PLCMngEntities context = CreateContext())
                {
                    PLC dbItem = context.PLC.FirstOrDefault(o => o.PLCID == id);
                    dbItem.IsConfirmed = false;
                    dbItem.ConfirmedDate = null;
                    dbItem.ConfirmedBy = null;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.PLCMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PLCMng.SearchFilterData data = new DTO.PLCMng.SearchFilterData();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.PLCMng.InitFormData GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PLCMng.InitFormData data = new DTO.PLCMng.InitFormData();
            data.Factories = new List<DTO.Support.Factory>();
            data.Items = new List<DTO.PLCMng.ItemForCreatePLC>();

            //try to get data
            try
            {
                data.Factories = supportFactory.GetAuthorizedFactories(userId).ToList();
                using (PLCMngEntities context = CreateContext())
                {
                    List<int> factoryIds = new List<int>();
                    factoryIds.AddRange(data.Factories.Select(o => o.FactoryID).ToList());
                    data.Items = converter.DB2DTO_ItemForCreatePLCList(context.PLCMng_ItemForCreatePLC_View.Where(o => o.FactoryID.HasValue && factoryIds.Contains(o.FactoryID.Value)).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.PLCMng.EditFormData GetData(int id, int bookingID, int factoryID, int offerLineID, int offerLineSparepartID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PLCMng.EditFormData data = new DTO.PLCMng.EditFormData();
            data.Data = new DTO.PLCMng.PLC();
            data.Data.PLCImages = new List<DTO.PLCMng.PLCImage>();
            data.PLCImageTypes = new List<DTO.Support.PLCImageType>();

            //try to get data
            try
            {
                using (PLCMngEntities context = CreateContext())
                {
                    // add new case
                    if (id == 0)
                    {
                        PLCMng_ItemForCreatePLC_View dbInfo = null;
                        if (offerLineID > 0)
                        {
                            dbInfo = context.PLCMng_ItemForCreatePLC_View.Where(o => o.BookingID == bookingID && o.FactoryID == factoryID && o.OfferLineID == offerLineID).FirstOrDefault();
                        }
                        else
                        {
                            dbInfo = context.PLCMng_ItemForCreatePLC_View.Where(o => o.BookingID == bookingID && o.FactoryID == factoryID && o.OfferLineSparepartID == offerLineSparepartID).FirstOrDefault();
                        }

                        if (dbInfo != null)
                        {
                            data.Data = converter.Merge(dbInfo);
                            data.Data.PLCImages = new List<DTO.PLCMng.PLCImage>();
                        }
                        else
                        {
                            throw new Exception("Item not exists!");
                        }
                    }
                    else
                    {
                        data.Data = converter.DB2DTO_PLC(context.PLCMng_PLC_View.Include("PLCMng_PLCImage_View").FirstOrDefault(o => o.PLCID == id));
                    }

                    data.PLCImageTypes = supportFactory.GetPLCImageTypes();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string GetReportData(string PLCIDs, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PLCMng_function_GetPLCReportDatas", new SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@PLCIDList", PLCIDs);
                adap.TableMappings.Add("Table", "PLCManagement2_PLCReport_View");
                adap.TableMappings.Add("Table1", "PLCManagement2_PLCImageReport_View");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "PLC");
                //return string.Empty;

                // generate xml file
                return DALBase.Helper.CreateReportFiles(ds, "PLC");
                //return Library.Helper.CreateCOMReportFileImportDataOnly(ds, "PLC");
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
