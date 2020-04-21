using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.MaterialTestingMng.DTO;

namespace Module.MaterialTestingMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        public DataFactory() { }

        private MaterialTestingMngEntities CreateContext()
        {
            return new MaterialTestingMngEntities(Library.Helper.CreateEntityConnectionString("DAL.MaterialTestingMngModel"));
        }
        public DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string materialTestReportUD = null;
                bool? isEnabled = null;
                string materialOptionUD = null;
                string materialOptionNM = null;
                string clientUD = null;
                string friendlyName = null;
                string testStandardNM = null;
                string testDate = null;
                

                if (filters.ContainsKey("MaterialTestReportUD") && !string.IsNullOrEmpty(filters["MaterialTestReportUD"].ToString()))
                {
                    materialTestReportUD = filters["MaterialTestReportUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("IsEnabled") && filters["IsEnabled"] != null && !string.IsNullOrEmpty(filters["IsEnabled"].ToString()))
                {
                    isEnabled = (filters["IsEnabled"].ToString() == "true") ? true : false;
                }
                if (filters.ContainsKey("MaterialOptionUD") && !string.IsNullOrEmpty(filters["MaterialOptionUD"].ToString()))
                {
                    materialOptionUD = filters["MaterialOptionUD"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("MaterialOptionNM") && !string.IsNullOrEmpty(filters["MaterialOptionNM"].ToString()))
                {
                    materialOptionNM = filters["MaterialOptionNM"].ToString().Replace("'", "''");
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
                using (MaterialTestingMngEntities context = CreateContext())
                {
                    totalRows = context.MaterialTestingMng_function_SearchMaterialTesting(materialTestReportUD,isEnabled, materialOptionUD, materialOptionNM, clientUD, friendlyName, testStandardNM, valTestDate, orderBy, orderDirection).Count();
                    var result = context.MaterialTestingMng_function_SearchMaterialTesting(materialTestReportUD, isEnabled, materialOptionUD, materialOptionNM, clientUD, friendlyName, testStandardNM, valTestDate, orderBy, orderDirection);
                    searchFormData.Data = converter.DB2DTO_MaterialTestReportSearch(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

        public List<DTO.SupportMaterialOptionDTO> GetMaterialOption(Hashtable filters, out Notification notification)
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
                    return converter.DB2DTO_GetMaterialOption(context.MaterialTestingMng_function_MaterialOption(searchString).ToList());
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
            data.Data = new MaterialTestReportDTO();
            data.Data.MaterialTestReportFileDTOs = new List<MaterialTestReportFileDTO>();
            data.Data.MaterialTestStandardDTOs = new List<MaterialTestStandardDTO>();
            data.SupportMaterialTestStandards = new List<SupportMaterialTestStandardDTO>();

            //OVER VIEW
            data.DataOverView = new MaterialTestReportOverViewDTO();
            data.DataOverView.MaterialTestReportFileOverViewDTOs = new List<MaterialTestReportFileOverViewDTO>();
            data.DataOverView.MaterialTestStandardOverViewDTOs = new List<MaterialTestStandardOverViewDTO>();

            try
            {
                using (MaterialTestingMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        MaterialTestingMng_MaterialTestReport_View dbItem = context.MaterialTestingMng_MaterialTestReport_View.FirstOrDefault(s => s.MaterialTestReportID == id);
                        MaterialTestingMng_MaterialTestReport_OverView dbItems = context.MaterialTestingMng_MaterialTestReport_OverView.FirstOrDefault(s => s.MaterialTestReportID == id);
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
                        data.Data = converter.DB2DTO_GetMaterialTestReport(dbItem);
                        data.DataOverView = converter.DB2DTO_GetOverViewMaterialTestReport(dbItems);
                    }
                    data.SupportMaterialTestStandards = converter.DB2DTO_GetSupportMaterialTestStandard(context.SupportMng_MaterialTestStandard_View.ToList());
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
            DTO.MaterialTestReportDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dto).ToObject<DTO.MaterialTestReportDTO>();
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (MaterialTestingMngEntities context = CreateContext())
                {
                    MaterialTestReport dbItem = null;
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM MaterialTestReport WITH (TABLOCKX, HOLDLOCK)");
                        try
                        {                        
                            if (id > 0)
                            {
                                dbItem = context.MaterialTestReport.FirstOrDefault(s => s.MaterialTestReportID == id);

                                if (dbItem == null)
                                {
                                    notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data" };
                                    return false;
                                }
                            }
                            else
                            {
                                dbItem = new MaterialTestReport();
                                context.MaterialTestReport.Add(dbItem);
                            }
                            converter.DTO2DB_MaterialTestReport(dtoItem, ref dbItem, FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", userId);
                            dbItem.UpdatedBy = userId;
                            dbItem.UpdatedDate = DateTime.Now;

                            context.MaterialTestReportFile.Local.Where(o => o.MaterialTestReport == null).ToList().ForEach(s => context.MaterialTestReportFile.Remove(s));
                            context.MaterialTestUsingMaterialStandard.Local.Where(o => o.MaterialTestReport == null).ToList().ForEach(o => context.MaterialTestUsingMaterialStandard.Remove(o));

                            context.SaveChanges();

                            //Create code of id
                            dbItem.MaterialTestReportUD = "M" + dbItem.MaterialTestReportID.ToString("D9");
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

                    dto = GetEdit(dbItem.MaterialTestReportID, out notification).Data;

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
                    var dbItem = context.MaterialTestReport.FirstOrDefault(o => o.MaterialTestReportID == id);
                    //MaterialTestReport dbItem = context.MaterialTestReport.FirstOrDefault(s => s.MaterialTestReportID == id);
                    if (dbItem == null)
                    {
                        notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                        return false;
                    }
                    context.MaterialTestReport.Remove(dbItem);
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
