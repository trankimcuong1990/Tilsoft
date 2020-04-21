using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Module.MaterialMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private MaterialMngEntities CreateContext()
        {
            return new MaterialMngEntities(Library.Helper.CreateEntityConnectionString("DAL.MaterialMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.MaterialSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    string MaterialUD = null;
                    string MaterialNM = null;
                    if (filters.ContainsKey("MaterialUD") && !string.IsNullOrEmpty(filters["MaterialUD"].ToString()))
                    {
                        MaterialUD = filters["MaterialUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("MaterialNM") && !string.IsNullOrEmpty(filters["MaterialNM"].ToString()))
                    {
                        MaterialNM = filters["MaterialNM"].ToString().Replace("'", "''");
                    }
                    totalRows = context.MaterialMng_function_SearchMaterial(MaterialUD, MaterialNM, orderBy, orderDirection).Count();
                    var result = context.MaterialMng_function_SearchMaterial(MaterialUD, MaterialNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_MaterialSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.Material();

            //try to get data
            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_Material(context.MaterialMng_Material_View.FirstOrDefault(o => o.MaterialID == id));
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

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    Material dbItem = context.Material.FirstOrDefault(o => o.MaterialID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Material not found!");
                    }

                    // check if ok to delete
                    ////

                    // everything ok, delete the task
                    context.Material.Remove(dbItem);
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.Material dtoMaterial = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.Material>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (MaterialMngEntities context = CreateContext())
                {
                    Material dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Material();
                        context.Material.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Material.FirstOrDefault(o => o.MaterialID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Material not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB(dtoMaterial, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                        context.SaveChanges();
                        dtoItem = GetData(dbItem.MaterialID, out notification).Data;
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
    }
}
