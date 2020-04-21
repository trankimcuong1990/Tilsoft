using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using Library;

namespace Module.FactorySpecificationMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private string _tempFolder;
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private FactorySpecificationMngEntities CreateContext()
        {
            return new FactorySpecificationMngEntities(Library.Helper.CreateEntityConnectionString("DAL.FactorySpecificationMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactorySpecificationMngEntities context = CreateContext())
                {
                    FactorySpecification dbItem = context.FactorySpecification.FirstOrDefault(o => o.FactorySpecificationID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sub Supplier not found!";
                        return false;
                    }
                    else
                    {
                        context.FactorySpecification.Remove(dbItem);
                        context.SaveChanges();

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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.FactorySpecification dtoFactorySpecification = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactorySpecification>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactorySpecificationMngEntities context = CreateContext())
                {
                    FactorySpecification dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new FactorySpecification();
                        context.FactorySpecification.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.FactorySpecification.FirstOrDefault(o => o.FactorySpecificationID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Factory Raw Material not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        //if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoFactorySpecification.ConcurrencyFlag_String)))
                        //{
                        //    throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        //}

                        // processing logo image
                        if (dtoFactorySpecification.FactoryFileLocation_HasChange)
                        {
                            dtoFactorySpecification.FactorySpecificationFileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoFactorySpecification.NewFile, dtoFactorySpecification.FactorySpecificationFileUD);
                        }

                        //convert dto to db
                        converter.DTO2BD(dtoFactorySpecification, ref dbItem);

                        context.FactorySpecificationComment.Local.Where(o => o.FactorySpecification == null).ToList().ForEach(o => context.FactorySpecificationComment.Remove(o));

                        dbItem.FactorySpecificationRemark = dtoFactorySpecification.FactorySpecificationRemark;

                        dbItem.FactorySpecificationUpdatedDate = DateTime.Now;
                        dbItem.FactorySpecificationUpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(userId, dbItem.FactorySpecificationID, out notification).Data;

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

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public DTO.EditFormData GetData(int iRequesterID, int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.FactorySpecification();
            data.Data.FactorySpecificationComments = new List<DTO.FactorySpecificationComment>();
            //try to get data
            try
            {
                if (id > 0)
                {
                    //check permission on factory
                    //if (fwFactory.CheckFactoryPermission(iRequesterID, id) == 0)
                    //{
                    //    throw new Exception("You do not have access permission on this");
                    //}

                    using (FactorySpecificationMngEntities context = CreateContext())
                    {
                        var dbItem = context.FactorySpecificationMng_FactorySpecification_View.FirstOrDefault(o => o.FactorySpecificationID == id);
                        if (dbItem == null)
                        {
                            throw new Exception("Can not found the data to edit");
                        }

                        data.Data = converter.DB2DTO_FactorySpecification(dbItem);

                        foreach (var item in data.Data.FactorySpecificationComments)
                        {
                            DateTime d1 = item.UpdatedDate.ConvertStringToDateTime().GetValueOrDefault(DateTime.Now);
                            DateTime d2 = DateTime.Now;

                            TimeSpan TimeSpan = d2.Subtract(d1);
                            int aDay = (int)TimeSpan.TotalDays;

                            if (item.UpdatedBy == iRequesterID && aDay <= 1)
                            {
                                item.CanDelete = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.SearchFilterData GetSearchFilter(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();

            try
            {
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public DTO.SearchFormData GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;

            int? UserID = userID;
            string Code = null;
            string ClientUD = null;
            string FactoryUD = null;
            string ItemDesc = null;
            string ProfomaInvoice = null;


            if (filters.ContainsKey("Code") && !string.IsNullOrEmpty(filters["Code"].ToString()))
            {
                Code = filters["Code"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("FactoryUD") && !string.IsNullOrEmpty(filters["FactoryUD"].ToString()))
            {
                FactoryUD = filters["FactoryUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ItemDesc") && !string.IsNullOrEmpty(filters["ItemDesc"].ToString()))
            {
                ItemDesc = filters["ItemDesc"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProfomaInvoice") && !string.IsNullOrEmpty(filters["ProfomaInvoice"].ToString()))
            {
                ProfomaInvoice = filters["ProfomaInvoice"].ToString().Replace("'", "''");
            }

            try
            {
                using (FactorySpecificationMngEntities context = CreateContext())
                {
                    totalRows = context.FactorySpecificationMng_function_SearchFactorySpecification(UserID, Code, ClientUD, FactoryUD, ItemDesc, ProfomaInvoice, orderBy, orderDirection).Count();
                    var result = context.FactorySpecificationMng_function_SearchFactorySpecification(UserID, Code, ClientUD, FactoryUD, ItemDesc, ProfomaInvoice, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_FactorySpecificationSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        //
        // Custom function
        //
        public List<DTO.FactoryOrderDetail> GetFactoryOrderDetail(int factoryID, int modelID, string articleCode, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactoryOrderDetail> data = new List<DTO.FactoryOrderDetail>();
            Module.Framework.BLL fwBll = new Module.Framework.BLL();
            try
            {
                using (FactorySpecificationMngEntities context = CreateContext())
                {
                    var result = context.FactorySpecificationMng_function_SearchFactoryOrderDetail(factoryID, modelID, articleCode).ToList();
                    data = converter.DB2DTO_FactoryOrderDetail(result);
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return data;
        }
        // Add new key raw material
        public List<DTO.FactorySpecificationComment> UpdateComment(int userId, object dtoItem, out Library.DTO.Notification notification)
        {
            List<DTO.FactorySpecificationComment> dtoCommnetList = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.FactorySpecificationComment>>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.FactorySpecificationComment> DTOCommentList = new List<DTO.FactorySpecificationComment>();

            try
            {
                using (FactorySpecificationMngEntities context = CreateContext())
                {
                    var dbItemList = context.FactorySpecificationComment.Select(o => o).ToList();

                    //Delete orphan
                    foreach (var item in dbItemList.ToArray())
                    {
                        if (!dtoCommnetList.Select(o => o.FactorySpecificationCommentID).Contains(item.FactorySpecificationCommentID))
                        {
                            if(item.UpdatedBy == userId)
                            {
                                context.FactorySpecificationComment.Remove(item);
                            }
                            else
                            {
                                throw new Exception("Unable to delete other user comment");
                            }
                            
                        }
                    }

                    //Map child row
                    foreach (var item in dtoCommnetList)
                    {
                        FactorySpecificationComment dbComment;
                        if (item.FactorySpecificationCommentID <= 0)
                        {
                            dbComment = new FactorySpecificationComment();
                            context.FactorySpecificationComment.Add(dbComment);
                            AutoMapper.Mapper.Map<DTO.FactorySpecificationComment, FactorySpecificationComment>(item, dbComment);

                            dbComment.UpdatedDate = DateTime.Now;
                            dbComment.UpdatedBy = userId;
                        }
                    }

                    context.SaveChanges();
                }
                DTOCommentList = GetCommentList(userId);
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return DTOCommentList;
        }

        public List<DTO.FactorySpecificationComment> GetCommentList(int userId)
        {
            List<DTO.FactorySpecificationComment> result = new List<DTO.FactorySpecificationComment>();
            try
            {
                using (FactorySpecificationMngEntities context = CreateContext())
                {
                    result = converter.DB2DTO_FactorySpecificationComment(context.FactorySpecificationMng_FactorySpecificationComment_View.ToList());
                    foreach (var item in result)
                    {
                        if(item.UpdatedBy == userId)
                        {
                            item.CanDelete = true;
                        }
                    }
                }
            }
            catch { }

            return result;
        }
    }
}
