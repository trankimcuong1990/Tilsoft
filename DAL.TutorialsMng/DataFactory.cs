using DAL.TutorialsMng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.TutorialsMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.Tutorials.SearchFormData, DTO.Tutorials.EditFormData, DTO.Tutorials.Tutorials>
    {
        private DataConverter convertor = new DataConverter();
        private TutorialsMngEntities CreateContext()
        {
            return new TutorialsMngEntities(DALBase.Helper.CreateEntityConnectionString("TutorialsMngModel"));
        }

        public override DTO.Tutorials.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.Tutorials.SearchFormData data = new DTO.Tutorials.SearchFormData();
            data.Data = new List<DTO.Tutorials.Tutorials>();
            totalRows = 0;


                    //string Name = null;

            //if (filters.ContainsKey("Name") && !string.IsNullOrEmpty(filters["Name"].ToString()))
            //{
            //    Name = filters["Name"].ToString().Replace("'", "''");
            //}
            


            //try to get data
            try
            {
                using (TutorialsMngEntities context = CreateContext())
                {
                    totalRows = context.TutorialsMng_function_getTutorials ().Count();
                    var result = context.TutorialsMng_function_getTutorials();
                    data.Data = convertor.DB2DTO_TutorialSearchResultList (result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.Tutorials.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.Tutorials.EditFormData data = new DTO.Tutorials.EditFormData();
            data.Data = new DTO.Tutorials.Tutorials();

            //try to get data
            try
            {
                using (TutorialsMngEntities context = CreateContext())
                {
                    data.Data = convertor.DB2DTO_Tutorial(context.TutorialsList_View.FirstOrDefault(o => o.ID == id));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
          //  throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (TutorialsMngEntities context = CreateContext())
                {
                    Tutorials  dbItem = context.Tutorials .FirstOrDefault(o => o.ID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Tutorial not found!";
                        return false;
                    }
                    else
                    {
                        context.Tutorials.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.Tutorials.Tutorials  dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (TutorialsMngEntities context = CreateContext())
                {
                    Tutorials dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Tutorials ();
                        context.Tutorials .Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Tutorials.FirstOrDefault(o => o.ID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Tutorial not found!";
                        return false;
                    }
                    else
                    {
                        convertor.DTO2BD_Tutorial(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ID, out notification).Data;

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
                    if (indexName == "TutorialNameUnique")
                    {
                        notification.Message = "The Tutorial Name already exists";
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

        public override bool Approve(int id, ref DTO.Tutorials.Tutorials dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.Tutorials.Tutorials dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }



        
    }
}
