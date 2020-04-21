using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ConstantEntryMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ConstantEntryMng.SearchFormData, DTO.ConstantEntryMng.EditFormData, DTO.ConstantEntryMng.ConstantEntry>
    {
        private DataConverter converter = new DataConverter();

                

        private ConstantEntryMngEntities CreateContext()
        {
            return new ConstantEntryMngEntities(DALBase.Helper.CreateEntityConnectionString("ConstantEntryMngModel"));
        }

        public override DTO.ConstantEntryMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ConstantEntryMng.SearchFormData data = new DTO.ConstantEntryMng.SearchFormData();
            data.Data = new List<DTO.ConstantEntryMng.ConstantEntrySearchResults>();
            data.EntryGroups = new List<DTO.ConstantEntryMng.EntryGroupList>();
            
            totalRows = 0;

            string EntryGroup = null;
            string DisplayText = null;

            if (filters.ContainsKey("EntryGroup"))
            {
                EntryGroup = filters["EntryGroup"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("DisplayText"))
            {
                DisplayText = filters["DisplayText"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (ConstantEntryMngEntities context = CreateContext())
                {
                    totalRows = context.ConstantEntryMng_function_SearchConstantEntry(DisplayText, EntryGroup, orderBy, orderDirection).Count();
                    var result = context.ConstantEntryMng_function_SearchConstantEntry( DisplayText, EntryGroup, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ConstantEntrySearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                    data.EntryGroups = GetEntryGroups().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.ConstantEntryMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ConstantEntryMng.EditFormData data = new DTO.ConstantEntryMng.EditFormData();
            data.Data = new DTO.ConstantEntryMng.ConstantEntry();
            data.EntryGroups  = new List<DTO.ConstantEntryMng.EntryGroupList>();

            //try to get data
            try
            {
                using (ConstantEntryMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_ConstantEntry(context.ConstantEntryMng_ConstantEntry_View

                            .FirstOrDefault(o => o.ConstantEntryID == id));
                   
                    }
                    data.EntryGroups = GetEntryGroups().ToList();
                    

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable <DTO.ConstantEntryMng.EntryGroupList> GetEntryGroups()
        {
            //try to get data
            try
            {
                using (ConstantEntryMngEntities context = CreateContext())
                {
                    return converter.DB2DTO_EntryGroups( context.ConstantEntryMng_ComboList.ToList());
                }
            }
            catch
            {
                return new List<DTO.ConstantEntryMng.EntryGroupList>();
            }
        }
        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ConstantEntryMngEntities context = CreateContext())
                {
                    ConstantEntry dbItem = context.ConstantEntry.FirstOrDefault(o => o.ConstantEntryID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "ConstantEntry not found!";
                        return false;
                    }
                    else
                    {
                        context.ConstantEntry.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.ConstantEntryMng.ConstantEntry dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;


            try
            {
                using (ConstantEntryMngEntities context = CreateContext())
                {
                    ConstantEntry dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ConstantEntry();
                        context.ConstantEntry.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ConstantEntry.FirstOrDefault(o => o.ConstantEntryID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "ConstantEntry not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_ConstantEntry(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ConstantEntryID, out notification).Data;

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
                    if (indexName == "DisplayTextUnique")
                    {
                        notification.Message = "Display Text  already exists";
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
        }

        public override bool Approve(int id, ref DTO.ConstantEntryMng.ConstantEntry dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ConstantEntryMng.ConstantEntry dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

    }
}
