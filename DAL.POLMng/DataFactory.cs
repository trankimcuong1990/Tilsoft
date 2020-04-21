using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.POLMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.POLMng.SearchFormData, DTO.POLMng.EditFormData, DTO.POLMng.POL>
    {
        private DataConverter converter = new DataConverter();
        private POLMngEntities CreateContext()
        {
            return new POLMngEntities(DALBase.Helper.CreateEntityConnectionString("POLMngModel"));
        }

        public override DTO.POLMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.POLMng.SearchFormData data = new DTO.POLMng.SearchFormData();
            data.Data = new List<DTO.POLMng.POLSearchResult>();
            totalRows = 0;

            string PoLUD = null;
            string PoLname = null;
            if (filters.ContainsKey("PoLUD") && !string.IsNullOrEmpty(filters["PoLUD"].ToString()))
            {
                PoLUD  = filters["PoLUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PoLname") && !string.IsNullOrEmpty(filters["PoLname"].ToString()))
            {
                PoLname = filters["PoLname"].ToString().Replace("'", "''");
            }


            //try to get data
            try
            {
                using (POLMngEntities context = CreateContext())
                {
                    totalRows = context.PoL_function_SearchPoL(PoLUD, PoLname, orderBy, orderDirection).Count();
                    var result = context.PoL_function_SearchPoL(PoLUD, PoLname, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_POLSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.POLMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.POLMng.EditFormData data = new DTO.POLMng.EditFormData();
            data.Data = new DTO.POLMng.POL();

            //try to get data
            try
            {
                using (POLMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_POL(context.List_POL_View.FirstOrDefault(o => o.PoLID == id));
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
                using (POLMngEntities context = CreateContext())
                {
                    POL dbItem = context.POL.FirstOrDefault(o => o.PoLID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Port of Loading not found!";
                        return false;
                    }
                    else
                    {
                        context.POL.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.POLMng.POL dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (POLMngEntities context = CreateContext())
                {
                    POL dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new POL();
                        context.POL.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.POL.FirstOrDefault(o => o.PoLID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Port of Loading  not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_POL(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.PoLID, out notification).Data;

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
                    if (indexName == "POLlUDUnique")
                    {
                        notification.Message = "This port of loading already exists";
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

        public override bool Approve(int id, ref DTO.POLMng.POL dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.POLMng.POL dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
