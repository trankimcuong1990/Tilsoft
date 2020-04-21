using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryMaterialNorm.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryMaterialNormEntities CreateContext()
        {
            return new FactoryMaterialNormEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryMaterialNormModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryMaterialNormEntities context = CreateContext())
                {
                    var dbItem = context.FactoryMaterialNorm.Where(o => o.FactoryMaterialNormID == id).FirstOrDefault();
                    foreach (var item in dbItem.FactoryMaterialNormDetail.ToArray())
                    {
                        context.FactoryMaterialNormDetail.Remove(item);
                    }
                    context.FactoryMaterialNorm.Remove(dbItem);
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

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            DTO.EditFormData editFormData = new DTO.EditFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            try
            {
                using (FactoryMaterialNormEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryMaterialNormMng_FactoryMaterialNorm_View dbItem;
                        dbItem = context.FactoryMaterialNormMng_FactoryMaterialNorm_View.FirstOrDefault(o => o.FactoryMaterialNormID == id);
                        editFormData.Data = converter.DB2DTO_FactoryMaterialNorm(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.FactoryMaterialNorm();
                        editFormData.Data.FactoryMaterialNormDetails = new List<DTO.FactoryMaterialNormDetail>();
                    }

                    //get support list
                    editFormData.Units = support_factory.GetUnit(1);
                    editFormData.FactoryGoodsProcedures = support_factory.GetFactoryGoodsProcedure();
                    return editFormData;
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
                return editFormData;
            }
        }

        public override DTO.SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            string articleCode = string.Empty;
            string description = string.Empty;
            if (filters.ContainsKey("articleCode")) articleCode = filters["articleCode"].ToString();
            if (filters.ContainsKey("description")) description = filters["description"].ToString();

            try
            {
                using (FactoryMaterialNormEntities context = CreateContext())
                {
                    totalRows = context.FactoryMaterialNormMng_function_SearchFactoryMaterialNorm(orderBy, orderDirection,articleCode,description).Count();
                    var result = context.FactoryMaterialNormMng_function_SearchFactoryMaterialNorm(orderBy, orderDirection, articleCode,description);
                    searchFormData.Data = converter.DB2DTO_FactoryMaterialNormSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryMaterialNorm dtoFactoryMaterialNorm = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryMaterialNorm>();
            try
            {
                using (FactoryMaterialNormEntities context = CreateContext())
                {
                    FactoryMaterialNorm dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryMaterialNorm();
                        context.FactoryMaterialNorm.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryMaterialNorm.Where(o => o.FactoryMaterialNormID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryMaterialNorm(dtoFactoryMaterialNorm, ref dbItem);
                        //remove orphan item
                        context.FactoryMaterialNormDetail.Local.Where(o => o.FactoryMaterialNorm == null).ToList().ForEach(o => context.FactoryMaterialNormDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryMaterialNormID, out notification).Data;
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
