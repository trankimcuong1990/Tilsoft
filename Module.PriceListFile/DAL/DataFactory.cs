using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceListFile.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private PriceListFileEntities CreateContext()
        {
            return new PriceListFileEntities(Library.Helper.CreateEntityConnectionString("DAL.PriceListFileModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string Season = null;
            string FileName = null;
            string Comment = null;

            if (filters.ContainsKey("Season") && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FileName") && !string.IsNullOrEmpty(filters["FileName"].ToString()))
            {
                FileName = filters["FileName"].ToString().Replace("'", "''");
            }

            if (filters.ContainsKey("Comment") && !string.IsNullOrEmpty(filters["Comment"].ToString()))
            {
                Comment = filters["Comment"].ToString().Replace("'", "''");
            }

            try
            {
                using (PriceListFileEntities context = CreateContext())
                {
                    totalRows = context.PriceListFile_function_SearchPriceListFile(Season, FileName, Comment, orderBy, orderDirection).Count();
                    var result = context.PriceListFile_function_SearchPriceListFile(Season, FileName, Comment, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_PriceListFileSearchList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.PriceListFile();
            data.Seasons = new List<Module.Support.DTO.Season>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (PriceListFileEntities context = CreateContext())
                    {
                        var w = context.PriceListFile_PriceListFile_View.FirstOrDefault(o => o.PriceListFileID == id);
                        if (w == null)
                        {
                            throw new Exception("Can not found the data to edit");
                        }
                        data.Data = converter.DB2DTO_PriceListFile(w);
                    }
                }
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PriceListFileEntities context = CreateContext())
                {
                    var dbItem = context.PriceListFile.Where(o => o.PriceListFileID == id).FirstOrDefault();
                    context.PriceListFile.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.PriceListFile dtoPriceListFile = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.PriceListFile>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.PriceListFile> PriceListFileDTOs = new List<DTO.PriceListFile>();
            try
            {
                using (PriceListFileEntities context = CreateContext())
                {
                    PriceListFile dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new PriceListFile();
                        context.PriceListFile.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PriceListFile.FirstOrDefault(o => o.PriceListFileID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Data not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoPriceListFile.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // processing pdf
                        if (dtoPriceListFile.PDFFileLocation_HasChange)
                        {
                            if (string.IsNullOrEmpty(dtoPriceListFile.NewPDFFile))
                            {
                                fwFactory.RemoveFile(dtoPriceListFile.PDFFileUD);
                            }
                            else
                            {
                                dtoPriceListFile.PDFFileUD = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoPriceListFile.NewPDFFile, dtoPriceListFile.PDFFileUD, dtoPriceListFile.PDFFriendlyName);
                            }
                        }

                        // processing excel
                        if (dtoPriceListFile.ExcelFileLocation_HasChange)
                        {
                            if (string.IsNullOrEmpty(dtoPriceListFile.NewExcelFile))
                            {
                                fwFactory.RemoveFile(dtoPriceListFile.ExcelFileUD);
                            }
                            else
                            {
                                dtoPriceListFile.ExcelFileUD = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoPriceListFile.NewExcelFile, dtoPriceListFile.ExcelFileUD, dtoPriceListFile.ExcelFriendlyName);
                            }
                        }
                        converter.DTO2DB(dtoPriceListFile, ref dbItem);

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.PriceListFileID, out notification).Data;

                        return true;
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

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }


        //
        // CUSTOM
        //
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Seasons = new List<Support.DTO.Season>();

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
    }
}
