using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
namespace Module.FactoryStepNorm.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();

        private FactoryStepNormEntities CreateContext()
        {
            return new FactoryStepNormEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryStepNormModel"));
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
                using (FactoryStepNormEntities context = CreateContext())
                {
                    var dbItem = context.FactoryStepNorm.Where(o => o.FactoryStepNormID == id).FirstOrDefault();
                    foreach (var item in dbItem.FactoryStepNormDetail.ToArray())
                    {
                        context.FactoryStepNormDetail.Remove(item);
                    }
                    context.FactoryStepNorm.Remove(dbItem);
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
                using (FactoryStepNormEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryStepNormMng_FactoryStepNorm_View dbItem;
                        dbItem = context.FactoryStepNormMng_FactoryStepNorm_View.FirstOrDefault(o => o.FactoryStepNormID == id);
                        editFormData.Data = converter.DB2DTO_FactoryStepNorm(dbItem);
                    }
                    else
                    {
                        editFormData.Data = new DTO.FactoryStepNorm();
                        editFormData.Data.FactoryStepNormDetails = new List<DTO.FactoryStepNormDetail>();
                    }

                    //get support list
                    editFormData.Units = support_factory.GetUnit(2);
                    editFormData.FactorySteps = support_factory.GetFactoryStep();
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
                using (FactoryStepNormEntities context = CreateContext())
                {
                    totalRows = context.FactoryStepNormMng_function_SearchFactoryStepNorm(orderBy, orderDirection,articleCode,description).Count();
                    var result = context.FactoryStepNormMng_function_SearchFactoryStepNorm(orderBy, orderDirection, articleCode,description);
                    searchFormData.Data = converter.DB2DTO_FactoryStepNormSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.FactoryStepNorm dtoFactoryStepNorm = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryStepNorm>();
            try
            {
                using (FactoryStepNormEntities context = CreateContext())
                {
                    FactoryStepNorm dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactoryStepNorm();
                        context.FactoryStepNorm.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactoryStepNorm.Where(o => o.FactoryStepNormID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "data not found!";
                        return false;
                    }
                    else
                    {
                        //convert dto to db
                        converter.DTO2DB_FactoryStepNorm(dtoFactoryStepNorm, ref dbItem);
                        //remove orphan item
                        context.FactoryStepNormDetail.Local.Where(o => o.FactoryStepNorm == null).ToList().ForEach(o => context.FactoryStepNormDetail.Remove(o));
                        //save data
                        context.SaveChanges();
                        //get return data
                        dtoItem = GetData(dbItem.FactoryStepNormID, out notification).Data;
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
