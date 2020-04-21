using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.TestStandardMng.DTO;

namespace Module.TestStandardMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreatContex())
                {
                    var dbItem = context.TestStandard.Where(o => o.TestStandardID == id).FirstOrDefault();
                    context.TestStandard.Remove(dbItem);
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

        public override EditForm GetData(int id, out Notification notification)
        {
            DTO.EditForm data = new DTO.EditForm();
            data.Data = new DTO.TestStandardDTO();        

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = CreatContex())
                {
                    if (id > 0)
                    {
                        TestStandard_TestStandard_View dbItem;
                        dbItem = context.TestStandard_TestStandard_View.FirstOrDefault(o => o.TestStandardID == id);
                        data.Data = converter.DB2DTO_TestStandard(dbItem);
                    }                  
                }
                return data;
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
                return data;
            }
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchForm data = new SearchForm();
            data.Data = new List<GetTestStandardDTO>();
            try
            {
                using (var context = CreatContex())
                {                  
                    data.Data = converter.DB2DTO_GetTestStandard(context.TestStandard_TestStandard_GetView.ToList());
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
            }
            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            DTO.TestStandardDTO dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.TestStandardDTO>();
            try
            {
                using (var context = CreatContex())
                {
                    TestStandard dbItem;
                    if (id == 0)
                    {
                        dbItem = new TestStandard();
                        context.TestStandard.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.TestStandard.Where(o => o.TestStandardID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "Data Not found !";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_TestStandard(dtoItems, ref dbItem);                      
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.TestStandardID, out notification).Data;
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

        private TestStandardMngEntities CreatContex()
        {
            return new TestStandardMngEntities(Library.Helper.CreateEntityConnectionString("DAL.TestStandardMngModel"));
        }


    }
}
