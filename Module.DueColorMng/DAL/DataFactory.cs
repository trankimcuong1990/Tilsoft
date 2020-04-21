using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.DueColorMng.DTO;

namespace Module.DueColorMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private DueColorMngEntities CreateContext()
        {
            return new DueColorMngEntities(Library.Helper.CreateEntityConnectionString("DAL.DueColorMngModel"));
        }

        public DTO.SearchFormData GetDataWithFilters(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.DueColorSearchDTO>();
            totalRows = 0;

            string DueColorUD = null;
            string DueColorNM = null;
            //if (filters.ContainsKey("DueColorUD") && !string.IsNullOrEmpty(filters["DueColorUD"].ToString()))
            //{
            //    DueColorUD = filters["DueColorUD"].ToString().Replace("'", "''");
            //}
            if (filters.ContainsKey("DueColorNM") && !string.IsNullOrEmpty(filters["DueColorNM"].ToString()))
            {
                DueColorNM = filters["DueColorNM"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (DueColorMngEntities context = CreateContext())
                {
                    totalRows = context.DueColorMng_function_SearchDueColor(DueColorNM, orderBy, orderDirection).Count();
                    var result = context.DueColorMng_function_SearchDueColor(DueColorNM, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_DueColorSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetEditData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.DueColorDTO();

            //try to get data
            try
            {
                using (DueColorMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_DueColor(context.DueColorMng_DueColor_View.FirstOrDefault(o => o.DueColorID == id));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public bool Delete(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (DueColorMngEntities context = CreateContext())
                {
                    DueColor dbItem = context.DueColor.FirstOrDefault(o => o.DueColorID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "MaterialColor not found!";
                        return false;
                    }
                    else
                    {
                        context.DueColor.Remove(dbItem);
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

        public bool Update(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            DTO.DueColorDTO dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.DueColorDTO>();
            try
            {
                using (var context = CreateContext())
                {
                    DueColor dueColor = null;

                    if (id == 0)
                    {
                        dueColor = new DueColor();

                        context.DueColor.Add(dueColor);
                    }
                    else
                    {
                        dueColor = context.DueColor.FirstOrDefault(o => o.DueColorID == id);
                    }

                    if (dueColor == null)
                    {
                        notification.Message = "Due Color not found!";

                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_DueColor(dtoItems, ref dueColor);
                        context.SaveChanges();

                        dtoItem = GetEditData(dueColor.DueColorID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
