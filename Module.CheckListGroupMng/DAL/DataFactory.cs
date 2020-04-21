using Library.DTO;
using Module.CheckListGroupMng.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CheckListGroupMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private CheckListGroupEntities CreateContext()
        {
            return new CheckListGroupEntities(Library.Helper.CreateEntityConnectionString("DAL.CheckListGroupMngModel"));
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification() { Type = NotificationType.Success };

            SearchFormData data = new SearchFormData() { Data = new List<CheckListGroupDTO>() };

            try
            {
                using (var context = CreateContext())
                {
                    string checkListGroupNM = null;
                    string checkListGroupUD = null;
                    string remark = null;
                    if (filters.ContainsKey("CheckListGroupNM") && !string.IsNullOrEmpty(filters["CheckListGroupNM"].ToString()))
                    {
                        checkListGroupNM = filters["CheckListGroupNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("CheckListGroupUD") && !string.IsNullOrEmpty(filters["CheckListGroupUD"].ToString()))
                    {
                        checkListGroupUD = filters["CheckListGroupUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Remark") && !string.IsNullOrEmpty(filters["Remark"].ToString()))
                    {
                        remark = filters["Remark"].ToString().Replace("'", "''");
                    }

                    totalRows = context.CheckListGroupMng_function_CheckListGroupSearchResult(checkListGroupUD, checkListGroupNM,remark, orderBy, orderDirection).Count();
                    data.Data = this.converter.BD2DTO_CheckListGroupMngSearchResult(context.CheckListGroupMng_function_CheckListGroupSearchResult(checkListGroupUD, checkListGroupNM, remark, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification()
                {
                    Type = NotificationType.Error,
                    Message = ex.Message

                };
            }
            return data;
        }
        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditFormData data = new EditFormData
            {
                CheckListGroupDTO = new CheckListGroupDTO()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var item = context.CheckListMng_CheckListGroup_View.FirstOrDefault(o => o.CheckListGroupID == id);
                        if (item == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        }
                        else
                        {
                            data.CheckListGroupDTO = this.converter.DB2DTO_CheckListGroupDTO(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
            }
            return data;

        }
        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var Context = CreateContext())
                {
                    CheckListGroup unit = Context.CheckListGroup.FirstOrDefault(o => o.CheckListGroupID == id);

                    if (unit == null)
                    {
                        notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }

                    Context.CheckListGroup.Remove(unit);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.CheckListGroupDTO checkListDTO = ((JObject)dtoItem).ToObject<DTO.CheckListGroupDTO>();

            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    CheckListGroup checkList = new CheckListGroup();

                    if (id == 0)
                    {
                        context.CheckListGroup.Add(checkList);
                    }

                    if (id > 0)
                    {
                        checkList = context.CheckListGroup.FirstOrDefault(o => o.CheckListGroupID == id);

                        if (checkList == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                            return false;
                        }
                    }

                    this.converter.DTO2DB_CheckListGroup(checkListDTO, ref checkList);
                    context.SaveChanges();

                    dtoItem = this.GetData(checkList.CheckListGroupID, out notification);
                }
                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }

        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
            //notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            //DTO.SupportFormData data = new DTO.SupportFormData();
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = Library.Helper.GetInnerException(ex).Message;
            //}
            //return data;
        }
    }
}
