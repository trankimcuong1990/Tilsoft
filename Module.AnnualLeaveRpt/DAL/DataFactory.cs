using Library.DTO;
using Module.AnnualLeaveRpt.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AnnualLeaveRpt.DAL
{
    internal class DataFactory2 : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private AnnualLeaveRptEntities CreateContext()
        {
            return new AnnualLeaveRptEntities(Library.Helper.CreateEntityConnectionString("DAL.AnnualLeaveRptModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(int userID, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {          
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<AnnualLeaveSearchResultDTO>();

            totalRows = 0;

            string EmployeeNM = null;
            int? CompanyID = null;
            int? AffectedYear = null;
            bool? HasLeft = false;

            if (filters.ContainsKey("employeeNM") && filters["employeeNM"] != null && !string.IsNullOrEmpty(filters["employeeNM"].ToString()))
            {
                EmployeeNM = filters["employeeNM"].ToString();
            }
            if (filters.ContainsKey("companyID") && filters["companyID"] != null && !string.IsNullOrEmpty(filters["companyID"].ToString()))
            {
                CompanyID = Convert.ToInt32(filters["companyID"].ToString());
            }
            if (filters.ContainsKey("affectedYear") && filters["affectedYear"] != null && !string.IsNullOrEmpty(filters["affectedYear"].ToString()))
            {
                AffectedYear = Convert.ToInt32(filters["affectedYear"].ToString());
            }
            if (filters.ContainsKey("HasLeft") && filters["HasLeft"] != null && !string.IsNullOrEmpty(filters["HasLeft"].ToString()))
            {
                HasLeft = (filters["HasLeft"].ToString() == "true") ? true : false;
            }
            //try to get data
            try
            {
                using (AnnualLeaveRptEntities context = CreateContext())
                {
                    int? companyID = fwFactory.GetCompanyID(userID);
                    var result = context.AnnualLeaveRpt_function_SearchAnnualLeave(EmployeeNM, CompanyID, AffectedYear, HasLeft, orderBy, orderDirection).Where(o=>o.CompanyID == companyID).ToList();
                    totalRows = result.Count();

                    data.Data = converter.DB2DTO_AnnualLeaveSearchResultDTO(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int userID, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //DTO.EditFormData data = new DTO.EditFormData();
            //data.Data = new DTO.SampleOrder();
            //data.Data.SampleOrderStatusID = 1;
            //data.Data.SampleOrderStatusNM = "PENDING";
            //data.Data.SampleProducts = new List<DTO.SampleProduct>();
            //data.Data.SampleMonitors = new List<DTO.SampleMonitor>();

            //data.Seasons = new List<Support.DTO.Season>();
            //data.SamplePurposes = new List<Support.DTO.SamplePurpose>();
            //data.SampleTransportTypes = new List<Support.DTO.SampleTransportType>();
            //data.Users = new List<Support.DTO.User>();
            //data.Factories = new List<Support.DTO.Factory>();
            //data.SampleRequestTypes = new List<Support.DTO.SampleRequestType>();
            //data.SampleProductStatuses = new List<Support.DTO.SampleProductStatus>();
            //data.SampleOrderStatuses = new List<Support.DTO.SampleOrderStatus>();


            ////try to get data
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        if (id > 0)
            //        {
            //            data.Data = converter.DB2DTO_SampleOrder(context.Sample2Mng_SampleOrder_View
            //                .Include("Sample2Mng_SampleProduct_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleItemLocation_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleReferenceImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductMinute_View.Sample2Mng_SampleProductMinuteImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleRemark_View.Sample2Mng_SampleRemarkImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleQARemark_View.Sample2Mng_SampleQARemarkImage_View")
            //                .Include("Sample2Mng_SampleMonitor_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProductSubFactory_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleProgress_View.Sample2Mng_SampleProgressImage_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View")
            //                .Include("Sample2Mng_SampleProduct_View.Sample2Mng_SampleTechnicalDrawing_View.Sample2Mng_SampleTechnicalDrawingFile_View")
            //                .FirstOrDefault(o => o.SampleOrderID == id));
            //        }

            //        data.Seasons = supportFactory.GetSeason().ToList();
            //        data.SamplePurposes = supportFactory.GetSamplePurpose().ToList();
            //        data.SampleTransportTypes = supportFactory.GetSampleTransportType().ToList();
            //        data.Users = supportFactory.GetUsers().ToList();
            //        data.Factories = supportFactory.GetFactory().ToList();
            //        data.SampleRequestTypes = supportFactory.GetSampleRequestType().ToList();
            //        data.SampleProductStatuses = supportFactory.GetSampleProductStatus().ToList();
            //        data.SampleProductStatuses.Remove(data.SampleProductStatuses.FirstOrDefault(o => o.SampleProductStatusID == 4)); // remove finished status
            //        data.SampleOrderStatuses = supportFactory.GetSampleOrderStatus().ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //}

            //return data;
        }

        public override bool DeleteData(int userID, int id, out Library.DTO.Notification notification)
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
            throw new NotImplementedException();

            //DTO.SampleOrder dtoOrder = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.SampleOrder>();
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleOrder dbItem = null;
            //        if (id == 0)
            //        {
            //            dbItem = new SampleOrder();
            //            context.SampleOrder.Add(dbItem);
            //            dbItem.CreatedBy = userId;
            //            dbItem.CreatedDate = DateTime.Now;
            //        }
            //        else
            //        {
            //            dbItem = context.SampleOrder.FirstOrDefault(o => o.SampleOrderID == id);
            //        }

            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Order not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.UpdatedBy = userId;
            //            dbItem.UpdatedDate = DateTime.Now;
            //            converter.DTO2DB_SampleOrder(dtoOrder, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
            //            context.SampleProductMinuteImage.Local.Where(o => o.SampleProductMinute == null).ToList().ForEach(o => context.SampleProductMinuteImage.Remove(o));
            //            context.SampleProductMinute.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProductMinute.Remove(o));
            //            context.SampleRemarkImage.Local.Where(o => o.SampleRemark == null).ToList().ForEach(o => context.SampleRemarkImage.Remove(o));
            //            context.SampleRemark.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleRemark.Remove(o));
            //            context.SampleTechnicalDrawingFile.Local.Where(o => o.SampleTechnicalDrawing == null).ToList().ForEach(o => context.SampleTechnicalDrawingFile.Remove(o));
            //            context.SampleTechnicalDrawing.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleTechnicalDrawing.Remove(o));
            //            context.SampleProgressImage.Local.Where(o => o.SampleProgress == null).ToList().ForEach(o => context.SampleProgressImage.Remove(o));
            //            context.SampleProgress.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleProgress.Remove(o));
            //            context.SampleReferenceImage.Local.Where(o => o.SampleProduct == null).ToList().ForEach(o => context.SampleReferenceImage.Remove(o));
            //            context.SampleProduct.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleProduct.Remove(o));
            //            context.SampleMonitor.Local.Where(o => o.SampleOrder == null).ToList().ForEach(o => context.SampleMonitor.Remove(o));
            //            context.SaveChanges();

            //            // generate order number
            //            if (id <= 0)
            //            {
            //                using (DbContextTransaction scope = context.Database.BeginTransaction())
            //                {
            //                    context.Database.ExecuteSqlCommand("SELECT * FROM SampleOrder WITH (TABLOCKX, HOLDLOCK)");

            //                    try
            //                    {
            //                        context.SaveChanges();
            //                        dbItem.SampleOrderUD = Library.Common.Helper.formatIndex(dbItem.SampleOrderID.ToString(), 8, "0");
            //                        context.SaveChanges();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        throw ex;
            //                    }
            //                    finally
            //                    {
            //                        scope.Commit();
            //                    }
            //                }
            //            }
            //            dtoItem = GetData(dbItem.SampleOrderID, out notification).Data;
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
        public override object GetInitData(int userID, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            int? companyID = fwFactory.GetCompanyID(userID);

            SupportFormData data = new SupportFormData();
            data.EmployeeDTOs = new List<EmployeeDTO>();
            data.CompanyDTOs = new List<CompanyDTO>();
            try
            {
                using (AnnualLeaveRptEntities context = CreateContext())
                {
                    data.EmployeeDTOs = converter.DB2DTO_EmployeeDTO(context.AnnualLeaveRpt_Employee_View.ToList());
                    data.CompanyDTOs = converter.DB2DTO_CompanyDTO(context.AnnualLeaveRpt_Company_View.Where(s=>s.CompanyID == companyID).ToList());
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return data;
            }
        }
        public List<DetailDTO> GetDetails(int employeeID, int affectedYear, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            List<DetailDTO> data = new List<DetailDTO>();
        
            try
            {
                using (AnnualLeaveRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_DetailDTO(context.AnnualLeaveRpt_function_GetTotal(employeeID, affectedYear).ToList());                  
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return data;
            }
        }

        public List<DetailTotalDTO> GetDetailTotal(int employeeID, int affectedYear, int type, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            List<DetailTotalDTO> data = new List<DetailTotalDTO>();

            try
            {
                using (AnnualLeaveRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_DetailTotalDTO(context.AnnualLeaveRpt_function_GetDetailTotal(employeeID, type, affectedYear).ToList());
                }

                return data;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return data;
            }
        }
       
        public override object GetSearchFilter(int userID, out Library.DTO.Notification notification)
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
