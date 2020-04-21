using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FrameMaterialProfileMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private FrameMaterialProfileMngEntities CreateContext()
        {
            return new FrameMaterialProfileMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FrameMaterialProfileMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.FrameMaterialProfileSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (FrameMaterialProfileMngEntities context = CreateContext())
                {
                    string frameMaterialProfileUD = null;
                    string description = null;
                    int? frameMaterialID = null;
                    string factory = null;

                    if (filters.ContainsKey("FrameMaterialProfileUD") && !string.IsNullOrEmpty(filters["FrameMaterialProfileUD"].ToString()))
                    {
                        frameMaterialProfileUD = filters["FrameMaterialProfileUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Description") && !string.IsNullOrEmpty(filters["Description"].ToString()))
                    {
                        description = filters["Description"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("FrameMaterialID") && !string.IsNullOrEmpty(filters["FrameMaterialID"].ToString()))
                    {
                        frameMaterialID = Convert.ToInt32(filters["FrameMaterialID"].ToString());
                    }
                    if (filters.ContainsKey("Factory") && !string.IsNullOrEmpty(filters["Factory"].ToString()))
                    {
                        factory = filters["Factory"].ToString().Replace("'", "''");
                    }

                    var dataTest = context.FrameMaterialProfileMng_function_SearchFrameMaterialProfile(frameMaterialProfileUD, description, frameMaterialID, factory, orderBy, orderDirection);
                    totalRows = dataTest.Count();
                    var result = context.FrameMaterialProfileMng_function_SearchFrameMaterialProfile(frameMaterialProfileUD, description, frameMaterialID, factory, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_FrameMaterialProfileSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FrameMaterialProfile();
            data.Data.FrameMaterialProfileFactories = new List<DTO.FrameMaterialProfileFactory>();
            data.Factories = new List<Module.Support.DTO.Factory>();
            data.FrameMaterials = new List<Module.Support.DTO.FrameMaterial>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (FrameMaterialProfileMngEntities context = CreateContext())
                    {
                        var profile = context.FrameMaterialProfileMng_FrameMaterialProfile_View.FirstOrDefault(o => o.FrameMaterialProfileID == id);
                        if (profile == null)
                        {
                            throw new Exception("Can not found the profile to edit");
                        }
                        data.Data = converter.DB2DTO_FrameMaterialProfile(profile);
                    }
                }
                data.Factories = supportFactory.GetFactory().ToList();
                data.FrameMaterials = supportFactory.GetFrameMaterial().ToList();
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
                using (FrameMaterialProfileMngEntities context = CreateContext())
                {
                    FrameMaterialProfile dbItem = context.FrameMaterialProfile.FirstOrDefault(o => o.FrameMaterialProfileID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Profile not found!";
                        return false;
                    }
                    else
                    {
                        context.FrameMaterialProfile.Remove(dbItem);
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
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FrameMaterialProfile dtoFrameMaterialProfile = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FrameMaterialProfile>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FrameMaterialProfileMngEntities context = CreateContext())
                {
                    FrameMaterialProfile dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FrameMaterialProfile();
                        context.FrameMaterialProfile.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FrameMaterialProfile.FirstOrDefault(o => o.FrameMaterialProfileID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Profile not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFrameMaterialProfile.ConcurrencyFlag_String)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2BD(dtoFrameMaterialProfile, ref dbItem);
                        context.SaveChanges();

                        // processing image
                        if (dtoFrameMaterialProfile.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._tempFolder, dtoFrameMaterialProfile.ImageFile_NewFile, dtoFrameMaterialProfile.ImageFile);
                        }
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.FrameMaterialProfileID, out notification).Data;

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
        public DTO.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //int i = 0;
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Factories = new List<Module.Support.DTO.Factory>();
            data.FrameMaterials = new List<Module.Support.DTO.FrameMaterial>();

            //try to get data
            try
            {
                data.Factories = supportFactory.GetFactory().ToList();
                data.FrameMaterials = supportFactory.GetFrameMaterial().ToList();
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
