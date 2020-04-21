using Library.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SampleItemMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormDataDTO, DTO.EditFormDataDTO>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        private DataConverter converter = new DataConverter();

        private SampleItemMngEntities CreateContext()
        {
            SampleItemMngEntities context = new SampleItemMngEntities(Library.Helper.CreateEntityConnectionString("DAL.SampleItemMngModel"));
            context.Database.CommandTimeout = 300;
            return context;
        }

        public override DTO.SearchFormDataDTO GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormDataDTO data = new DTO.SearchFormDataDTO();
            data.Data = new List<DTO.SampleItemSearchResultDTO>();
            totalRows = 0;

            string SampleOrderUD = null;
            string Season = null;
            string ClientUD = null;  
            int? SampleProductStatusID = null;
            int UserID; 
            string SampleItemCode = null;
            string SampleItemName = null;           

            if (filters.ContainsKey("SampleOrderUD") && !string.IsNullOrEmpty(filters["SampleOrderUD"].ToString()))
            {
                SampleOrderUD = filters["SampleOrderUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Season") && filters["Season"] != null && !string.IsNullOrEmpty(filters["Season"].ToString()))
            {
                Season = filters["Season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SampleProductStatusID") && filters["SampleProductStatusID"] != null && !string.IsNullOrEmpty(filters["SampleProductStatusID"].ToString()))
            {
                SampleProductStatusID = Convert.ToInt32(filters["SampleProductStatusID"].ToString());
            }
            if (filters.ContainsKey("SampleItemCode") && !string.IsNullOrEmpty(filters["SampleItemCode"].ToString()))
            {
                SampleItemCode = filters["SampleItemCode"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("SampleItemName") && !string.IsNullOrEmpty(filters["SampleItemName"].ToString()))
            {
                SampleItemName = filters["SampleItemName"].ToString().Replace("'", "''");
            }
            UserID = Convert.ToInt32(filters["UserID"].ToString());
           
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {
                    totalRows = context.SampleItemMng_function_SearchSampleItem(SampleOrderUD, Season, ClientUD, SampleProductStatusID, UserID, orderBy, orderDirection, SampleItemCode, SampleItemName).Count();
                    var result = context.SampleItemMng_function_SearchSampleItem(SampleOrderUD, Season, ClientUD, SampleProductStatusID, UserID, orderBy, orderDirection, SampleItemCode, SampleItemName);
                    data.Data = converter.DB2DTO_SampleItemSearchResultDTOs(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }

        public override DTO.EditFormDataDTO GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;
            DTO.EditFormDataDTO data = new DTO.EditFormDataDTO();
            data.Data = new DTO.SampleProductDTO();
            data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.SampleItemMng_SampleProduct_View
                        .Include("SampleItemMng_FactoryBreakdown_View")
                        .Include("SampleItemMng_SampleProgress_View")
                        .Include("SampleItemMng_SampleProgress_View.SampleItemMng_SampleProgressImage_View")
                        .Include("SampleItemMng_SampleQARemark_View")
                        .Include("SampleItemMng_SampleQARemark_View.SampleItemMng_SampleQARemarkImage_View")
                        .FirstOrDefault(o => o.SampleProductID == id);

                    data.Data = AutoMapper.Mapper.Map<SampleItemMng_SampleProduct_View, DTO.SampleProductDTO>(dbItem);
                    data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
                    // only leave status which can be selected by QA
                    foreach (Support.DTO.SampleProductStatus status in data.SampleProductStatuses.Where(o => o.SampleProductStatusID != 4 && o.SampleProductStatusID != 6).ToArray())
                    {
                        data.SampleProductStatuses.Remove(status);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public bool UpdateProgressData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleProgressDTO dtoProgress = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleProgressDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {
                    SampleProgress dbItem = null;
                    if (id <= 0)
                    {
                        dbItem = new SampleProgress();
                        context.SampleProgress.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.SampleProgress.FirstOrDefault(o => o.SampleProgressID == id);
                    }
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;

                    if (dbItem == null)
                    {
                        notification.Message = "Sample Progress not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_SampleProgress(dtoProgress, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                        //// notification
                        //SendNotification(context);

                        // remove orphan
                        context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
                        context.SaveChanges();

                        int newID = dbItem.SampleProgressID;
                        dtoItem = converter.DB2DTO_SampleProgress(context.SampleItemMng_SampleProgress_View.Include("SampleItemMng_SampleProgressImage_View").FirstOrDefault(o => o.SampleProgressID == newID));
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return false;
            }
        }
        public bool DeleteProgress(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {
                    // check if can delete
                    SampleProgress dbItem = context.SampleProgress.FirstOrDefault(o => o.SampleProgressID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Progress not found");
                    }

                    // everything ok, delete
                    // remove attached file
                    foreach (SampleProgressImage dbImage in dbItem.SampleProgressImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbImage.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbImage.FileUD);
                        }
                        dbItem.SampleProgressImage.Remove(dbImage);
                    }
                    context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
                    context.SampleProgress.Remove(dbItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;             
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
                return false;
            }           

           return true;
        }
        public bool UpdateQARemarkData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleQARemarkDTO dtoRemark = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleQARemarkDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {
                    SampleProduct dbProduct = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == dtoRemark.SampleProductID);
                    if (dbProduct == null)
                    {
                        throw new Exception("Sample not found");
                    }

                    SampleQARemark dbItem = new SampleQARemark();
                    dbProduct.SampleQARemark.Add(dbItem);
                    dbItem.UpdatedBy = userId;
                    dbItem.UpdatedDate = DateTime.Now;
                    converter.DTO2DB_SampleQARemark(dtoRemark, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);

                    //// notification
                    //SendNotification(context);

                    // update
                    context.SaveChanges();

                    int newID = dbItem.SampleQARemarkID;
                    dtoItem = converter.DB2DTO_SampleQARemark(context.SampleItemMng_SampleQARemark_View.Include("SampleItemMng_SampleQARemarkImage_View").FirstOrDefault(o => o.SampleQARemarkID == newID));
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool DeleteQARemark(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {
                    // check if can delete
                    SampleQARemark dbItem = context.SampleQARemark.FirstOrDefault(o => o.SampleQARemarkID == id);
                    if (dbItem == null)
                    {
                        throw new Exception("Remark not found");
                    }

                    // everything ok, delete
                    // remove attached file
                    foreach (SampleQARemarkImage dbImage in dbItem.SampleQARemarkImage.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbImage.FileUD))
                        {
                            fwFactory.RemoveImageFile(dbImage.FileUD);
                        }
                        dbItem.SampleQARemarkImage.Remove(dbImage);
                    }
                    context.SampleQARemarkImage.Local.Where(o => o.SampleQARemark == null).ToList().ForEach(o => context.SampleQARemarkImage.Remove(o));
                    context.SampleQARemark.Remove(dbItem);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (SampleItemMngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.SampleTechnicalDrawing.Remove(dbItem);
            //            // also remove all child item if needed
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.SampleProductDTO dtoSampleProduct = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleProductDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {                    
                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);                    

                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoSampleProduct.ConcurrencyFlag)))
                        {
                            throw new Exception(Library.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }
                        converter.DTO2DB_SampleProduct(dtoSampleProduct, ref dbItem);

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.SampleProductID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (SampleItemMngEntities context = CreateContext())
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
            //    using (SampleItemMngEntities context = CreateContext())
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

        public bool UpdateProductStatus(int userId, int id, int statusId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {
                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.SampleProductStatusID = statusId;
                        dbItem.StatusUpdatedBy = userId;
                        dbItem.StatusUpdatedDate = DateTime.Now;

                        // delete finished image if status is: 1,2,3,10 ~ CREATE,CONFIRMED,REVISED,REJECTED
                        int[] statuses = { 1, 2, 3, 10 };
                        if (statuses.Contains(statusId))
                        {
                            if (!string.IsNullOrEmpty(dbItem.FinishedImage))
                            {
                                fwFactory.RemoveImageFile(dbItem.FinishedImage);
                                dbItem.FinishedImage = string.Empty;
                            }
                        }

                        // notification
                        //SendNotification(context);

                        context.SaveChanges();

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddSampleItem(null, id); // table lockx and also check if item is available on sql server side

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public bool UpdateProductStatus2(int userId, int id, int statusId, string file, out Library.DTO.Notification notification)
        {
            // FINISH STATUS
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (SampleItemMngEntities context = CreateContext())
                {
                 
                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Product not found!";
                        return false;
                    }
                    else
                    {

                        dbItem.SampleProductStatusID = statusId;
                        dbItem.StatusUpdatedBy = userId;
                        dbItem.StatusUpdatedDate = DateTime.Now;

                        // update file
                        if (!string.IsNullOrEmpty(file))
                        {
                            dbItem.FinishedImage = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", file, dbItem.FinishedImage);
                        }

                        // add product item
                        int totalExistingItem = dbItem.SampleProductItem.Count();
                        for (int index = 1; index <= dbItem.Quantity.Value - totalExistingItem; index++)
                        {
                            SampleProductItem dbProductItem = new SampleProductItem();
                            dbProductItem.SampleProductItemUD = dbItem.SampleProductUD + "-" + index.ToString("D2");
                            dbProductItem.CreatedDate = DateTime.Now;
                            dbItem.SampleProductItem.Add(dbProductItem);
                        }

                        //// notification
                        //SendNotification(context);

                        context.SaveChanges();

                        // add item to quotation if needed
                        context.FW_Quotation_function_AddSampleItem(null, id); // table lockx and also check if item is available on sql server side

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
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
            //    using (SampleItemMngEntities context = CreateContext())
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

        public DTO.SearchFilterDataDTO GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterDataDTO data = new DTO.SearchFilterDataDTO();
            data.SampleProductStatusDTOs = new List<DTO.SampleProductStatusDTO>();
            data.SeasonDTOs = new List<DTO.SeasonDTO>();

            try
            {
                data.SampleProductStatusDTOs = GetSampleProductStatusDTOs();
                data.SeasonDTOs = GetSeasonDTOs();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return data;
        }

        public List<DTO.SampleProductStatusDTO> GetSampleProductStatusDTOs()
        {
            List<DTO.SampleProductStatusDTO> result;
            using (SampleItemMngEntities context = this.CreateContext())
            {
                result = converter.DB2DTO_SampleProductStatusDTOs(context.SampleItemMng_SampleProductStatus_View.ToList());
            }
            return result;
        }
        public List<DTO.SeasonDTO> GetSeasonDTOs()
        {
            List<DTO.SeasonDTO> data = new List<DTO.SeasonDTO>();
            for (int i = 2005; i <= DateTime.Now.Year + 1; i++)
            {
                data.Add(new DTO.SeasonDTO() { SeasonValue = i.ToString() + "/" + (i + 1).ToString(), SeasonText = i.ToString() + "/" + (i + 1).ToString() });
            }
            var result = data.OrderByDescending(f => f.SeasonValue);

            return result.ToList();
        }
       
    }
}
