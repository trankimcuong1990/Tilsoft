using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.QualityDocumentMng.DTO;
using Newtonsoft.Json.Linq;

namespace Module.QualityDocumentMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchForm, DTO.EditForm>
    {

        Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        DataConverter converter = new DataConverter();

        private Support.DAL.DataFactory spfactory = new Support.DAL.DataFactory();

        public QualityDocumentMngEntities CreatContex()
        {
            return new QualityDocumentMngEntities(Library.Helper.CreateEntityConnectionString("DAL.QualityDocumentMngModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };
            try
            {
                using (var context = CreatContex())
                {
                    var dbItem = context.QualityDocument.Where(o => o.QualityDocumentID == id).FirstOrDefault();
                    context.QualityDocument.Remove(dbItem);
                    context.SaveChanges();
                }
                return true;
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

        public override EditForm GetData(int id, out Notification notification)
        {
            DTO.EditForm editForm = new EditForm();
            editForm.Data = new DTO.QualityDocumentDTO();
            editForm.supportQualityDocuments = new List<SupportQualityDocument>();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = CreatContex())
                {
                    if (id > 0)
                    {
                        QualityDocumentMng_QualityDocument_View dbItem;
                        dbItem = context.QualityDocumentMng_QualityDocument_View.FirstOrDefault(o => o.QualityDocumentID == id);
                        editForm.Data = converter.DB2DTO_QualityDocument(dbItem);
                    }

                    editForm.supportQualityDocuments = converter.DB2DTO_GetQualityType(context.SupportMng_QualityDocumentType_View.ToList());
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
            }

            return editForm;
        }

        public override SearchForm GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchForm data = new SearchForm();
            data.Data = new List<QualityDocumentSearchDTO>();

            string issuedDate = null;

            string qualityDocumentUD = filters["QualityDocumentUD"]?.ToString().Replace("'", "''");
            string description = filters["Description"]?.ToString().Replace("'", "''");
            if (filters.ContainsKey("IssuedDate") && !string.IsNullOrEmpty(filters["IssuedDate"].ToString()))
            {
                issuedDate = filters["IssuedDate"].ToString().Replace("'", "''");
            }
            string qualityDocumentTypeNM = filters["QualityDocumentTypeNM"]?.ToString().Replace("'", "''");
            string friendlyName = filters["FriendlyName"]?.ToString().Replace("'", "''");

            DateTime? issuedDateTime = issuedDate.ConvertStringToDateTime();

            try
            {
                using (var context = CreatContex())
                {
                    totalRows = context.QualityDocumentMng_Function_QualityDocumentSearchView(qualityDocumentUD, description, qualityDocumentTypeNM, friendlyName, issuedDateTime, orderBy, orderDirection).Count();
                    var result = context.QualityDocumentMng_Function_QualityDocumentSearchView(qualityDocumentUD, description, qualityDocumentTypeNM, friendlyName, issuedDateTime, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_SearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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

            }
            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            DTO.QualityDocumentDTO dtoItems = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.QualityDocumentDTO>();

            try
            {
                using (var context = CreatContex())
                {
                    QualityDocument dbItem;

                    if (id == 0)
                    {
                        dbItem = new QualityDocument();
                        context.QualityDocument.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.QualityDocument.Where(o => o.QualityDocumentID == id).FirstOrDefault();
                    }
                    if (dbItem == null)
                    {
                        notification.Message = "Data Not Found !";
                        return false;
                    }
                    else
                    {
                        converter.DTO2DB_QualityDocument(dtoItems, ref dbItem);

                        //upload file
                        Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
                        string tempFolder = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        if (dtoItems.File_HasChange.HasValue && dtoItems.File_HasChange.Value)
                        {
                            dbItem.AttachedFile = fwFactory.CreateFilePointer(tempFolder, dtoItems.File_NewFile, dtoItems.AttachedFile, dtoItems.FriendlyName);
                        }

                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;

                        //save
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.QualityDocumentID, out notification).Data;
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
            }

            return true;
        }
    }
}
