using Module.ComplianceMng.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ComplianceMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private ComplianceMngEntities CreateContext()
        {
            return new ComplianceMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ComplianceMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            DTO.SearchFormData searchFormData = new DTO.SearchFormData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            try
            {
                string fromDate = null;
                string toDate = null;
                int? factoryID = null;
                string complianceProcessUD = null;
                int? complianceCertificateTypeID = null;
                int? auditStatusID = null;
                string clientUD = null;
                string clientNM = null;

                if (filters.ContainsKey("fromDate") && !string.IsNullOrEmpty(filters["fromDate"].ToString()))
                {
                    fromDate = filters["fromDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("toDate") && !string.IsNullOrEmpty(filters["toDate"].ToString()))
                {
                    toDate = filters["toDate"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("factoryID") && filters["factoryID"] != null)
                {
                    factoryID = Convert.ToInt32(filters["factoryID"]);
                }

                if (filters.ContainsKey("complianceProcessUD") && !string.IsNullOrEmpty(filters["complianceProcessUD"].ToString()))
                {
                    complianceProcessUD = filters["complianceProcessUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("complianceCertificateTypeID") && filters["complianceCertificateTypeID"] != null)
                {
                    complianceCertificateTypeID = Convert.ToInt32(filters["complianceCertificateTypeID"]);
                }

                if (filters.ContainsKey("auditStatusID") && filters["auditStatusID"] != null)
                {
                    auditStatusID = Convert.ToInt32(filters["auditStatusID"]);
                }

                if (filters.ContainsKey("clientUD") && !string.IsNullOrEmpty(filters["clientUD"].ToString()))
                {
                    clientUD = filters["clientUD"].ToString().Replace("'", "''");
                }

                if (filters.ContainsKey("clientNM") && !string.IsNullOrEmpty(filters["clientNM"].ToString()))
                {
                    clientNM = filters["clientNM"].ToString().Replace("'", "''");
                }

                using (ComplianceMngEntities context = CreateContext())
                {
                    var result = context.ComplianceMng_function_SearchCompliance(fromDate, toDate, factoryID, complianceProcessUD, complianceCertificateTypeID, auditStatusID, clientUD, clientNM, orderBy, orderDirection).ToList();                   
                    totalRows = result.Count(); 
                    searchFormData.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    //get support list
                    searchFormData.FactoryDTOs = converter.DB2DTO_Factory(context.ComplianceMng_Factory_View.ToList());
                    searchFormData.ClientDTOs = converter.DB2DTO_Client(context.ComplianceMng_Client_View.ToList());
                    searchFormData.ComplianceCertificateTypeDTOs = converter.DB2DTO_ComplianceCertificateType(context.ComplianceMng_ComplianceCertificateType_View.ToList());
                    searchFormData.AuditStatusDTOs = converter.DB2DTO_AuditStatus(context.ComplianceMng_AuditStatus_View.ToList());

                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return searchFormData;
        }
        public override EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            //try to get data
            try
            {
                using (ComplianceMngEntities context = CreateContext())
                {
                    EditFormData data = new EditFormData();
                    data.ComplianceProcessDTO = new ComplianceProcessDTO();
                    data.FactoryDTOs = converter.DB2DTO_Factory(context.ComplianceMng_Factory_View.ToList());
                    data.ClientDTOs = converter.DB2DTO_Client(context.ComplianceMng_Client_View.ToList());
                    data.ComplianceCertificateTypeDTOs = converter.DB2DTO_ComplianceCertificateType(context.ComplianceMng_ComplianceCertificateType_View.ToList());
                    data.AuditStatusDTOs = converter.DB2DTO_AuditStatus(context.ComplianceMng_AuditStatus_View.ToList());
                    data.EmployeeDTOs = converter.DB2DTO_Employee(context.ComplianceMng_Employee_View.ToList());

                    if (id > 0)
                    {
                        ComplianceMng_Compliance_View dbItem = context.ComplianceMng_Compliance_View
                            .Include("ComplianceMng_ComplianceAttachedFile_View")
                            .Include("ComplianceMng_CompliancePIC_View")
                            .FirstOrDefault(o => o.ComplianceProcessID == id);
                       
                        data.ComplianceProcessDTO = converter.DB2DTO_ComplianceProcessDTO(dbItem);                     
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return new EditFormData();
            }
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
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
            ComplianceProcessDTO inputParam = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<ComplianceProcessDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
            try
            {
                using (ComplianceMngEntities context = CreateContext())
                {
                    ComplianceProcess dbItem = null;                 
                    if (id == 0)
                    {
                        dbItem = new ComplianceProcess();
                        context.ComplianceProcess.Add(dbItem);

                        dbItem.ComplianceProcessUD = context.ComplianceMng_function_GenerateCode().FirstOrDefault();
                    }
                    else
                    {
                        //get db item
                        dbItem = context.ComplianceProcess.FirstOrDefault(o => o.ComplianceProcessID == id);                       
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Comliance not found!";
                        return false;
                    }
                    else
                    {
                    
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                      
                        // process Detail
                        foreach (DTO.ComplianceAttachedFileDTO dtoDetail in inputParam.ComplianceAttachedFileDTOs)
                        {                            
                            // FileUD
                            if (dtoDetail.FileUDHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewFileUD))
                                {
                                    if (dtoDetail.FileUD == dtoDetail.NewFileUD)
                                    {
                                        dtoDetail.FileUD = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.FileUD);
                                    }                                    
                                }
                                else
                                {                                   
                                    dtoDetail.FileUD = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewFileUD, dtoDetail.FileUD, dtoDetail.FileUDFriendlyName);                                   
                                }
                            }
                        }
                        foreach (DTO.CompliancePICDTO dtoDetail in inputParam.CompliancePICDTOs)
                        {
                            // FileUD
                            if (dtoDetail.FileUDHasChange)
                            {
                                if (string.IsNullOrEmpty(dtoDetail.NewFileUD))
                                {
                                    if (dtoDetail.FileUD == dtoDetail.NewFileUD)
                                    {
                                        dtoDetail.FileUD = null;
                                    }
                                    else
                                    {
                                        fwFactory.RemoveFile(dtoDetail.FileUD);
                                    }
                                }
                                else
                                {
                                    dtoDetail.FileUD = fwFactory.CreateNoneImageFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoDetail.NewFileUD, dtoDetail.FileUD, dtoDetail.FileUDFriendlyName);
                                }
                            }
                        }

                        //read dto to db
                        converter.DTO2DB_ComplianceProcess(inputParam, ref dbItem, userId);                        

                        //delete offerline is null
                        context.ComplianceAttachedFile.Local.Where(o => o.ComplianceProcess == null).ToList().ForEach(o => context.ComplianceAttachedFile.Remove(o));
                        context.CompliancePIC.Local.Where(o => o.ComplianceProcess == null).ToList().ForEach(o => context.CompliancePIC.Remove(o));
                        //save data
                        context.SaveChanges();
                     
                        //reload data                   
                        if (id > 0)
                        {
                            dtoItem = new ComplianceProcessDTO { ComplianceProcessID = id };
                        }
                        else
                        {
                            dtoItem = new ComplianceProcessDTO { ComplianceProcessID = dbItem.ComplianceProcessID };
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
            }

            return false;
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

        public List<DTO.CalendarSearchDTO> GetCalendar(int id, out Library.DTO.Notification notification)
        {
            List<DTO.CalendarSearchDTO> Data = new List<DTO.CalendarSearchDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };           
            try
            {
                using (ComplianceMngEntities context = CreateContext())
                {
                    var result = context.ComplianceMng_CalendarResult_View.ToList();
                    Data = converter.DB2DTO_Calendar(result);               
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return Data;
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
