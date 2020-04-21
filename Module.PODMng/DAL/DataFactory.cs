using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;

namespace Module.PODMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private PODMngEntities CreateContext()
        {
            return new PODMngEntities(Library.Helper.CreateEntityConnectionString("DAL.PODMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.PODSearchResult>();
            totalRows = 0;

            string PoDUD = null;
            string PoDName = null;
            if (filters.ContainsKey("PoDUD") && !string.IsNullOrEmpty(filters["PoDUD"].ToString()))
            {
                PoDUD = filters["PoDUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PoDName") && !string.IsNullOrEmpty(filters["PoDName"].ToString()))
            {
                PoDName = filters["PoDName"].ToString().Replace("'", "''");
            }

            try
            {
                using (PODMngEntities context = CreateContext())
                {
                    totalRows = context.PODMng_function_SearchPOD(PoDUD, PoDName, orderBy, orderDirection).Count();
                    var result = context.PODMng_function_SearchPOD(PoDUD, PoDName, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_PODSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            data.Data = new DTO.POD();

            try
            {
                using (PODMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_POD(context.PODMng_POD_View.FirstOrDefault(o => o.PoDID == id));
                    data.ClientCountries = GetClientCountry().ToList();
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
                using (PODMngEntities context = CreateContext())
                {
                    POD dbItem = context.POD.FirstOrDefault(o => o.PoDID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "POD not found!";
                        return false;
                    }
                    else
                    {
                        context.POD.Remove(dbItem);
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

        public bool UpdateData(int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;
            try
            {
                DTO.POD dtoPOD = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.POD>();
                using (PODMngEntities context = CreateContext())
                {
                    POD dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new POD();
                        context.POD.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.POD.FirstOrDefault(o => o.PoDID == id);
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "POD not found";
                        return false;
                    }
                    else
                    {
                        converter.DB2DTO_POD(dtoPOD, ref dbItem);
                        context.SaveChanges();
                        dtoItem = GetData(dbItem.PoDID, out notification).Data;
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
                    if (indexName == "PoDUDUniqe")
                    {
                        notification.Message = "The POD Code is already exists";
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DTO.ListClientCountryDTO> GetClientCountry()
        {
            //try to get data
            try
            {
                using (PODMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_ListClientCountry(context.LIst_ClientCountry_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.ListClientCountryDTO>();
            }
        }
    }
}
