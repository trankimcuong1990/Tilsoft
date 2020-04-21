using Library.DTO;
using Module.ComplianceCertificateTypeMng.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ComplianceCertificateTypeMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private ComplianceCertificateTypeEntities CreateContext()
        {
            return new ComplianceCertificateTypeEntities(Library.Helper.CreateEntityConnectionString("DAL.ComplianceCertificateTypeModel"));
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification() { Type = NotificationType.Success };

            SearchFormData data = new SearchFormData() { Data = new List<ComplianceCertificateTypeDTO>() };

            try
            {
                using (var context = CreateContext())
                {
                    string complianceCertificateTypeUD = null;
                    string complianceCertificateTypeNM = null;
                    bool? isRequired = null;
                    
                    if (filters.ContainsKey("ComplianceCertificateTypeUD") && !string.IsNullOrEmpty(filters["ComplianceCertificateTypeUD"].ToString()))
                    {
                        complianceCertificateTypeUD = filters["ComplianceCertificateTypeUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ComplianceCertificateTypeNM") && !string.IsNullOrEmpty(filters["ComplianceCertificateTypeNM"].ToString()))
                    {
                        complianceCertificateTypeNM = filters["ComplianceCertificateTypeNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsRequired") && filters["IsRequired"] != null && !string.IsNullOrEmpty(filters["IsRequired"].ToString()))
                    {
                        isRequired = (filters["IsRequired"].ToString() == "true") ? true : false;
                    }


                    totalRows = context.ComplianceCertificateTypeMng_function_ComplianceCertificateTypeSearchResult(complianceCertificateTypeUD, complianceCertificateTypeNM, isRequired, orderBy, orderDirection).Count();
                    data.Data = this.converter.DB2DTO_ComplianceCertificateTypeSearch(context.ComplianceCertificateTypeMng_function_ComplianceCertificateTypeSearchResult(complianceCertificateTypeUD, complianceCertificateTypeNM, isRequired, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                ComplianceCertificateTypeDTO = new ComplianceCertificateTypeDTO()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = CreateContext())
                    {
                        var item = context.ComplianceCertificateTypeMng_ComplianceCertificateType_View.FirstOrDefault(o => o.ComplianceCertificateTypeID == id);
                        if (item == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        }
                        else
                        {
                            data.ComplianceCertificateTypeDTO = this.converter.DB2DTO_ComplianceCertificateTypeDTO(item);
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

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var Context = CreateContext())
                {
                    ComplianceCertificateType unit = Context.ComplianceCertificateType.FirstOrDefault(o => o.ComplianceCertificateTypeID == id);

                    if (unit == null)
                    {
                        notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }
                    var item = Context.ComplianceCertificateTypeMng_ComplianceCertificateTypeCheck_View.Where(o => o.ComplianceCertificateTypeID == id).FirstOrDefault();
                    //CheckPermission
                    if (item.isUsed.Value == true)
                    {
                        throw new Exception("You can't delete because it's used");
                    }

                    Context.ComplianceCertificateType.Remove(unit);
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

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.ComplianceCertificateTypeDTO ComplianceCertificateTypeDTO = ((JObject)dtoItem).ToObject<DTO.ComplianceCertificateTypeDTO>();

            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = CreateContext())
                {
                    ComplianceCertificateType complianceCertificateType = new ComplianceCertificateType();

                    if (id == 0)
                    {
                        context.ComplianceCertificateType.Add(complianceCertificateType);
                    }

                    if (id > 0)
                    {
                        complianceCertificateType = context.ComplianceCertificateType.FirstOrDefault(o => o.ComplianceCertificateTypeID == id);

                        if (complianceCertificateType == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                            return false;
                        }
                        
                    }
                    
                    this.converter.DTO2DB_ComplianceCertificateType(ComplianceCertificateTypeDTO, ref complianceCertificateType);
                    context.SaveChanges();

                    dtoItem = this.GetData(complianceCertificateType.ComplianceCertificateTypeID, out notification);
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
