using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.CushionTestingMng.DTO;

namespace Module.CushionTestingMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }
        private CushionTestingMngEntities CreateContext()
        {
            return new CushionTestingMngEntities(Library.Helper.CreateEntityConnectionString("DAL.CushionTestingMngModel"));
        }
        public DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string cushionTestReportUD = null;
                bool? isEnabled = null;
                string cushionColorUD = null;
                string cushionColorNM = null;
                string clientUD = null;
                string friendlyName = null;
                string testStandardNM = null;
                string testDate = null;


                if (filters.ContainsKey("CushionTestReportUD") && !string.IsNullOrEmpty(filters["CushionTestReportUD"].ToString()))
                {
                    cushionTestReportUD = filters["CushionTestReportUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("IsEnabled") && filters["IsEnabled"] != null && !string.IsNullOrEmpty(filters["IsEnabled"].ToString()))
                {
                    isEnabled = (filters["IsEnabled"].ToString() == "true") ? true : false;
                }
                if (filters.ContainsKey("CushionColorUD") && !string.IsNullOrEmpty(filters["CushionColorUD"].ToString()))
                {
                    cushionColorUD = filters["CushionColorUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("CushionColorNM") && !string.IsNullOrEmpty(filters["CushionColorNM"].ToString()))
                {
                    cushionColorNM = filters["CushionColorNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                {
                    clientUD = filters["ClientUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("FriendlyName") && !string.IsNullOrEmpty(filters["FriendlyName"].ToString()))
                {
                    friendlyName = filters["FriendlyName"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("TestStandardNM") && !string.IsNullOrEmpty(filters["TestStandardNM"].ToString()))
                {
                    testStandardNM = filters["TestStandardNM"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("TestDate") && !string.IsNullOrEmpty(filters["TestDate"].ToString()))
                {
                    testDate = filters["TestDate"].ToString().Replace("'", "''");
                }
                DateTime? valTestDate = testDate.ConvertStringToDateTime();
                using (CushionTestingMngEntities context = CreateContext())
                {
                    totalRows = context.CushionTestingMng_function_SearchCushionTesting(cushionTestReportUD, isEnabled, cushionColorUD, cushionColorNM, clientUD, friendlyName, testStandardNM, valTestDate, orderBy, orderDirection).Count();
                    var result = context.CushionTestingMng_function_SearchCushionTesting(cushionTestReportUD, isEnabled, cushionColorUD, cushionColorNM, clientUD, friendlyName, testStandardNM, valTestDate, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_CushionTestReportSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public List<DTO.SupportCushionColorDTO> GetCushionColor(Hashtable filters, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                string searchString = (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString().Replace("'", "''"))) ? filters["searchQuery"].ToString() : null;

                using (var context = CreateContext())
                {
                    return converter.DB2DTO_GetCushionColor(context.CushionTestingMng_function_CushionColor(searchString).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return null;
            }
        }


        public EditFormData GetEdit(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            EditFormData data = new EditFormData();
            data.Data = new CushionTestReportDTO();
            data.Data.CushionTestReportFileDTOs = new List<CushionTestReportFileDTO>();
            data.Data.CushionTestStandardDTOs = new List<CushionTestStandardDTO>();
            data.SupportCushionTestStandards = new List<SupportCushionTestStandardDTO>();

            //OVER VIEW
            data.DataOverView = new CushionTestReportOverViewDTO();
            data.DataOverView.CushionTestReportFileOverViewDTOs = new List<CushionTestReportFileOverViewDTO>();
            data.DataOverView.CushionTestStandardOverViewDTOs = new List<CushionTestStandardOverViewDTO>();

            try
            {
                using (CushionTestingMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        CushionTestingMng_CushionTestReport_View dbItem = context.CushionTestingMng_CushionTestReport_View.FirstOrDefault(s => s.CushionTestReportID == id);
                        CushionTestingMng_CushionTestReport_OverView dbItems = context.CushionTestingMng_CushionTestReport_OverView.FirstOrDefault(s => s.CushionTestReportID == id);
                        if (dbItem == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                            return data;
                        }
                        if (dbItems == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                            return data;
                        }
                        data.Data = converter.DB2DTO_GetCushionTestReport(dbItem);
                        data.DataOverView = converter.DB2DTO_GetOverViewCushionTestReport(dbItems);
                    }
                    data.SupportCushionTestStandards = converter.DB2DTO_GetSupportCushionTestStandard(context.SupportMng_CushionTestStandard_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return data;
            }
            return data;
        }

        public bool Update(int userId, int id, ref object dto, out Notification notification)
        {
            DTO.CushionTestReportDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dto).ToObject<DTO.CushionTestReportDTO>();
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (CushionTestingMngEntities context = CreateContext())
                {
                    CushionTestReport dbItem = null;
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM CushionTestReport WITH (TABLOCKX, HOLDLOCK)");
                        try
                        {
                            if (id > 0)
                            {
                                dbItem = context.CushionTestReport.FirstOrDefault(s => s.CushionTestReportID == id);

                                if (dbItem == null)
                                {
                                    notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data" };
                                    return false;
                                }
                            }
                            else
                            {
                                dbItem = new CushionTestReport();
                                context.CushionTestReport.Add(dbItem);
                            }
                            converter.DTO2DB_CushionTestReport(dtoItem, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;

                            context.CushionTestReportFile.Local.Where(o => o.CushionTestReport == null).ToList().ForEach(s => context.CushionTestReportFile.Remove(s));
                            context.CushionTestReportUsingCushionStandard.Local.Where(o => o.CushionTestReport == null).ToList().ForEach(o => context.CushionTestReportUsingCushionStandard.Remove(o));

                            context.SaveChanges();

                            //Create code of id
                            dbItem.CushionTestReportUD = "C" + dbItem.CushionTestReportID.ToString("D9");
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }
                    }

                    dto = GetEdit(dbItem.CushionTestReportID, out notification).Data;

                }
                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        public bool Delete(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.CushionTestReport.FirstOrDefault(o => o.CushionTestReportID == id);
                    if (dbItem == null)
                    {
                        notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                        return false;
                    }
                    context.CushionTestReport.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
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
