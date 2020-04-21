using Library.DTO;
using Module.CheckListMng.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.CheckListMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private CheckListMngEntities CreateContext()
        {
            return new CheckListMngEntities(Library.Helper.CreateEntityConnectionString("DAL.CheckListMngModel"));
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification() { Type = NotificationType.Success };

            SearchFormData data = new SearchFormData() { Data = new List<CheckListDTO>() };

            try
            {
                using (var context = CreateContext())
                {
                    string checkListNM = null;
                    string checkListUD = null;
                    string checkListName = null;
                    string typeName = null;
                    int? checkListGroupID = null;
                    int? typeOfInspecID = null;
                    if (filters.ContainsKey("CheckListNM") && !string.IsNullOrEmpty(filters["CheckListNM"].ToString()))
                    {
                        checkListNM = filters["CheckListNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("CheckListUD") && !string.IsNullOrEmpty(filters["CheckListUD"].ToString()))
                    {
                        checkListUD = filters["CheckListUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("CheckListName") && !string.IsNullOrEmpty(filters["CheckListName"].ToString()))
                    {
                        checkListName = filters["CheckListName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("TypeName") && !string.IsNullOrEmpty(filters["TypeName"].ToString()))
                    {
                        typeName = filters["TypeName"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("CheckListGroupID") && filters["CheckListGroupID"] != null)
                    {
                        checkListGroupID = Convert.ToInt32(filters["CheckListGroupID"]);
                    }
                    if (filters.ContainsKey("TypeOfInspecID") && filters["TypeOfInspecID"] != null)
                    {
                        typeOfInspecID = Convert.ToInt32(filters["TypeOfInspecID"]);
                    }
                    
                    totalRows = context.CheckListMng_function_CheckListSearchResult(checkListUD, checkListNM,checkListName,typeName, checkListGroupID, typeOfInspecID, orderBy, orderDirection).Count();
                    data.Data = this.converter.BD2DTO_CheckListMngSearchResult(context.CheckListMng_function_CheckListSearchResult(checkListUD, checkListNM,checkListName, typeName, checkListGroupID, typeOfInspecID, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        //public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        //{
        //    throw new NotImplementedException();

        //    //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    //DTO.EditFormData data = new DTO.EditFormData();
        //    //data.Data = new DTO.SampleOrder();
        //    //data.Data.SampleOrderStatusID = 1;
        //    //data.Data.SampleOrderStatusNM = "PENDING";
        //    //data.Data.SampleProducts = new List<DTO.SampleProduct>();
        //    //data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

        //    //data.Seasons = new List<Support.DTO.Season>();
        //    //data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
        //    //data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
        //    //data.Users = new List<Support.DTO.User>();
        //    //data.Factories = new List<Support.DTO.Factory>();
        //    //data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
        //    //data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
        //    //data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();


        //    ////try to get data
        //    //try
        //    //{
        //    //    using (Sample2MngEntities context = CreateContext())
        //    //    {
        //    //        if (id > 0)
        //    //        {
        //    //            data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
        //    //                .Include("Sample2Mng_SampleProduct_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleItemLocation_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View.Sample2Mng_SampleQARemarkImage_View")
        //    //                .Include("Sample2Mng_SampleMonitor_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductSubFactory_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
        //    //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
        //    //                .FirstOrDefault(o => o.SampleOrderID == id));
        //    //        }

        //    //        data.Seasons = supportFactory.GetSeason().ToList();
        //    //        data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
        //    //        data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
        //    //        data.Users = supportFactory.GetUsers().ToList();
        //    //        data.Factories = supportFactory.GetFactory().ToList();
        //    //        data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();
        //    //        data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
        //    //        data.SampleProductStatuses.Remove(data.SampleProductStatuses.FirstOrDefault(o => o.SampleProductStatusID == 4)); // remove finished status
        //    //        data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notification.Type = Library.DTO.NotificationType.Error;
        //    //    notification.Message = ex.Message;
        //    //}

        //    //return data;
        //}

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditFormData data = new EditFormData
            {
                CheckListDTO = new CheckListDTO()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var item = context.CheckListMng_CheckList_View.FirstOrDefault(o => o.CheckListID == id);
                        if (item == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        }
                        else
                        {
                            data.CheckListDTO = this.converter.DB2DTO_CheckListDTO(item);
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
                    CheckList unit = Context.CheckList.FirstOrDefault(o => o.CheckListID == id);

                    if (unit == null)
                    {
                        notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }

                    Context.CheckList.Remove(unit);
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
            DTO.CheckListDTO checkListDTO = ((JObject)dtoItem).ToObject<DTO.CheckListDTO>();

            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    CheckList checkList = new CheckList();

                    if (id == 0)
                    {
                        context.CheckList.Add(checkList);
                    }

                    if (id > 0)
                    {
                        checkList = context.CheckList.FirstOrDefault(o => o.CheckListID == id);

                        if (checkList == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                            return false;
                        }
                    }

                    this.converter.DTO2DB_CheckList(checkListDTO, ref checkList);
                    context.SaveChanges();

                    dtoItem = this.GetData(checkList.CheckListID, out notification);
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
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            try
            {
                using (CheckListMngEntities context = CreateContext())
                {
                    data.CheckListGroups = converter.DB2DTO_CheckList(context.CheckListMng_CheckListGroup_View.ToList());
                }
                using (CheckListMngEntities context = CreateContext())
                {
                    data.TypeOfInspectionDTO = converter.DB2DTO_Support_TypeOfInspection(context.Support_TypeOfInspection_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }
            return data;
        }
    }
}
