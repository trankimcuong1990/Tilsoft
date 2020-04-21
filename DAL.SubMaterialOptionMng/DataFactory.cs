using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SubMaterialOptionMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.SubMaterialOptionMng.SearchFormData, DTO.SubMaterialOptionMng.EditFormData, DTO.SubMaterialOptionMng.SubMaterialOption>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private SubMaterialOptionMngEntities CreateContext()
        {
            return new SubMaterialOptionMngEntities(DALBase.Helper.CreateEntityConnectionString("SubMaterialOptionMngModel"));
        }

        public override DTO.SubMaterialOptionMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialOptionMng.SearchFormData data = new DTO.SubMaterialOptionMng.SearchFormData();
            data.Data = new List<DTO.SubMaterialOptionMng.SubMaterialOptionSearchResult>();
            totalRows = 0;

            string SubMaterialOptionUD = null;
            string SubMaterialOptionNM = null;
            string Season = null;
            bool? IsStandard = null;
            bool? IsEnabled = null;
            if (filters.ContainsKey("SubMaterialOptionUD") && !string.IsNullOrEmpty(filters["SubMaterialOptionUD"].ToString()))
            {
                SubMaterialOptionUD = filters["SubMaterialOptionUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SubMaterialOptionNM") && !string.IsNullOrEmpty(filters["SubMaterialOptionNM"].ToString()))
            {
                SubMaterialOptionNM = filters["SubMaterialOptionNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsStandard") && filters["IsStandard"] != null && !string.IsNullOrEmpty(filters["IsStandard"].ToString()))
            {
                IsStandard = (filters["IsStandard"].ToString() == "true") ? true : false;
            }
            if (filters.ContainsKey("IsEnabled") && filters["IsEnabled"] != null && !string.IsNullOrEmpty(filters["IsEnabled"].ToString()))
            {
                IsEnabled = (filters["IsEnabled"].ToString() == "true") ? true : false;
            }

            //try to get data
            try
            {
                using (SubMaterialOptionMngEntities context = CreateContext())
                {
                    totalRows = context.SubMaterialOptionMng_function_SearchSubMaterialOption(SubMaterialOptionUD, SubMaterialOptionNM, Season, IsStandard, IsEnabled, orderBy, orderDirection).Count();
                    var result = context.SubMaterialOptionMng_function_SearchSubMaterialOption(SubMaterialOptionUD, SubMaterialOptionNM, Season, IsStandard, IsEnabled, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SubMaterialOptionSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.SubMaterialOptionMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialOptionMng.EditFormData data = new DTO.SubMaterialOptionMng.EditFormData();
            data.Data = new DTO.SubMaterialOptionMng.SubMaterialOption() { ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg" };
            data.SubMaterialColors = new List<DTO.Support.SubMaterialColor>();
            data.SubMaterials = new List<DTO.Support.SubMaterial>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                using (SubMaterialOptionMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_SubMaterialOption(context.SubMaterialOptionMng_SubMaterialOption_View.FirstOrDefault(o => o.SubMaterialOptionID == id));
                    }

                    data.SubMaterialColors = supportFactory.GetSubMaterialColor().ToList();
                    data.SubMaterials = supportFactory.GetSubMaterial().ToList();
                    data.Seasons = supportFactory.GetSeason().ToList();
                }
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SubMaterialOptionMngEntities context = CreateContext())
                {
                    SubMaterialOption dbItem = context.SubMaterialOption.FirstOrDefault(o => o.SubMaterialOptionID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sub material option not found!";
                        return false;
                    }
                    else
                    {
                        // processing image
                        if (!string.IsNullOrEmpty(dbItem.ImageFile))
                        {
                            Library.FileHelper.ImageManager imgMng = new Library.FileHelper.ImageManager(FrameworkSetting.Setting.AbsoluteFileFolder, FrameworkSetting.Setting.AbsoluteThumbnailFolder);

                            // delete phisycal files 
                            string toBeDeletedFileLocation = "";
                            string toBeDeletedThumbnailLocation = "";
                            fwFactory.GetDBFileLocation(dbItem.ImageFile, out toBeDeletedFileLocation, out toBeDeletedThumbnailLocation);

                            if (!string.IsNullOrEmpty(toBeDeletedFileLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedFileLocation);
                                }
                                catch { }
                            }

                            if (!string.IsNullOrEmpty(toBeDeletedThumbnailLocation))
                            {
                                try
                                {
                                    imgMng.DeleteFile(toBeDeletedThumbnailLocation);
                                }
                                catch { }
                            }
                        }

                        context.SubMaterialOption.Remove(dbItem);
                        context.SaveChanges();

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

        public override bool UpdateData(int id, ref DTO.SubMaterialOptionMng.SubMaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (SubMaterialOptionMngEntities context = CreateContext())
                {
                    SubMaterialOption dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new SubMaterialOption();
                        context.SubMaterialOption.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SubMaterialOption.FirstOrDefault(o => o.SubMaterialOptionID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Sub material option not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoItem, ref dbItem);
                        context.SaveChanges();

                        // processing image
                        if (dtoItem.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.ImageFile_NewFile, dtoItem.ImageFile);
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.SubMaterialOptionID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if (indexName == "SubMaterialColorUnique")
                    {
                        notification.Message = "The combination of Sub Material and Sub Material Color is already exists";
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.SubMaterialOptionMng.SubMaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.SubMaterialOptionMng.SubMaterialOption dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.SubMaterialOptionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialOptionMng.SearchFilterData data = new DTO.SubMaterialOptionMng.SearchFilterData();
            data.Seasons = new List<DTO.Support.Season>();
            data.YesNoValues = new List<DTO.Support.YesNo>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
                data.YesNoValues = supportFactory.GetYesNo().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.SubMaterialOptionMng.EditFormData GetData(int id, int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SubMaterialOptionMng.EditFormData data = new DTO.SubMaterialOptionMng.EditFormData();
            data.Data = new DTO.SubMaterialOptionMng.SubMaterialOption() { ImageFile_DisplayUrl = FrameworkSetting.Setting.ThumbnailUrl + "no-image.jpg" };
            data.SubMaterialColors = new List<DTO.Support.SubMaterialColor>();
            data.SubMaterials = new List<DTO.Support.SubMaterial>();
            data.Seasons = new List<DTO.Support.Season>();

            //try to get data
            try
            {
                using (SubMaterialOptionMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_SubMaterialOption(context.SubMaterialOptionMng_SubMaterialOption_View.FirstOrDefault(o => o.SubMaterialOptionID == id));
                    }
                    //else
                    //{
                    //    data.Data.UpdatedBy = userId;
                    //    data.Data.UpdatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    //    var employee = context.Employee.Where(s => s.UserID.HasValue && s.UserID.Value == userId).FirstOrDefault();
                    //    data.Data.UpdatorName = (employee == null) ? string.Empty : employee.EmployeeNM;
                    //}

                    data.SubMaterialColors = supportFactory.GetSubMaterialColor().ToList();
                    data.SubMaterials = supportFactory.GetSubMaterial().ToList();
                    data.Seasons = supportFactory.GetSeason().ToList();
                }
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
