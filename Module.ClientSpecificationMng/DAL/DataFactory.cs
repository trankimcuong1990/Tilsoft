using Library.Base;
using Module.ClientSpecificationMng.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using System.Collections;
using Library;
using FrameworkSetting;
using System.Data.SqlClient;

namespace Module.ClientSpecificationMng.DAL
{
    internal class DataFactory : DataFactory<SearchFormDTO, EditFormDTO>
    {
        private ClientSpecificationMngEntities CreateContext()
        {
            return new ClientSpecificationMngEntities(Helper.CreateEntityConnectionString("DAL.ClientSpecificationMngModel"));
        }

        private DataConverter converter = new DataConverter();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormDTO GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItems, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            bool isUpdated = true;

            ClientSpecificationDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoItems).ToObject<ClientSpecificationDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    ClientSpecification dbItem;

                    dbItem = context.ClientSpecification.FirstOrDefault(o => o.ClientSpecificationID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Cannot be found data!";

                        return false;
                    }

                    if (dtoItem.HasChange)
                    {
                        string pathFile = ("eurofar_standard.docx".Equals(dtoItem.FriendlyName)) ? Setting.AbsoluteUserTempFolder + @"\" : Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";
                        dtoItem.ClientSpecificationFileUD = fwFactory.CreateNoneImageFilePointer(pathFile, dtoItem.NewFile, dtoItem.ClientSpecificationFileUD, dtoItem.FriendlyName);
                    }

                    converter.DTO2DB_ClientSpecification(dtoItem, ref dbItem);

                    dbItem.ClientSpecificationUpdatedBy = userId;
                    dbItem.ClientSpecificationUpdatedDate = DateTime.Now;

                    context.SaveChanges();

                    dtoItems = GetData(userId, dbItem.ClientSpecificationID, out notification).ResultData;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                isUpdated = false;
            }

            return isUpdated;
        }

        public SearchFormDTO GetDataWithFilter(int userID, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification();
            notification.Type = NotificationType.Success;

            SearchFormDTO data = new SearchFormDTO();
            data.ResultData = new List<ClientSpecificationSearchResultDTO>();

            string clientSpecificationUD;
            string clientUD;
            string itemDesc;
            string proformaInvoice;

            try
            {
                clientSpecificationUD = (filters.ContainsKey("clientSpecificationCode") && filters["clientSpecificationCode"] != null && !string.IsNullOrEmpty(filters["clientSpecificationCode"].ToString())) ? filters["clientSpecificationCode"].ToString().Trim() : null;
                clientUD = (filters.ContainsKey("clientUD") && filters["clientUD"] != null && !string.IsNullOrEmpty(filters["clientUD"].ToString())) ? filters["clientUD"].ToString().Trim() : null;
                itemDesc = (filters.ContainsKey("itemDesc") && filters["itemDesc"] != null && !string.IsNullOrEmpty(filters["itemDesc"].ToString())) ? filters["itemDesc"].ToString().Trim() : null;
                proformaInvoice = (filters.ContainsKey("proformaInvoice") && filters["proformaInvoice"] != null && !string.IsNullOrEmpty(filters["proformaInvoice"].ToString())) ? filters["proformaInvoice"].ToString().Trim() : null;

                using (var context = CreateContext())
                {
                    context.ClientSpecificationMng_function_AutomaticClient(userID);
                    context.ClientSpecificationMng_function_AutomaticFactory(userID);

                    totalRows = context.ClientSpecificationMng_function_GetDataWithFilter(userID, clientSpecificationUD, clientUD, itemDesc, proformaInvoice, orderBy, orderDirection).Count();

                    data.ResultData = converter.DB2DTO_ClientSpecificationSearchResult(context.ClientSpecificationMng_function_GetDataWithFilter(userID, clientSpecificationUD, clientUD, itemDesc, proformaInvoice, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public FactorySpecificationCommentDTO PostComment(int userID, object dtoItems, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            FactorySpecificationCommentDTO dtoItem = ((Newtonsoft.Json.Linq.JObject)dtoItems).ToObject<FactorySpecificationCommentDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    FactorySpecificationComment dbItem = new FactorySpecificationComment();

                    context.FactorySpecificationComment.Add(dbItem);

                    converter.DTO2DB_FactorySpecificationComment(dtoItem, ref dbItem);

                    dbItem.UpdatedBy = userID;
                    dbItem.UpdatedDate = DateTime.Now;

                    context.SaveChanges();

                    dtoItem = converter.DB2DTO_FactorySpecificationComment(context.ClientSpecificationMng_FactoryMngFactoryComment_View.FirstOrDefault(o => o.FactorySpecificationCommentID == dbItem.FactorySpecificationCommentID));

                    int subDays = DateTime.Now.Subtract(dtoItem.UpdatedDate.ConvertStringToDateTime().Value).Days;
                    dtoItem.CanDelete = (userID == dtoItem.UpdatedBy && subDays <= 1);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return null;
            }

            return dtoItem;
        }

        public int? DeleteComment(int userID, int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var data = context.ClientSpecificationMng_function_DeleteFactorySpecificationComment(id, userID).FirstOrDefault();

                    if (data == 99)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "You cannot delete this comment. Because it is of yours";

                        return data;
                    }

                    context.SaveChanges();

                    return data;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return null;
            }
        }

        public EditFormDTO GetData(int userID, int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            EditFormDTO data = new EditFormDTO();
            data.ResultData = new ClientSpecificationDTO();
            data.ClientSpecificationTypes = new List<ClientSpecificationTypeDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.ClientSpecificationMng_Sale_View.FirstOrDefault(o => o.ClientSpecificationID == id);

                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not be found data!";

                        return data;
                    }

                    data.ResultData = converter.DB2DTO_ClientSpecification(dbItem);
                    data.ClientSpecificationTypes = converter.DB2DTO_ClientSpecificationType(context.ClientSpecificationMng_Type_View.ToList());

                    foreach (var item1 in data.ResultData.FactorySpecifications)
                    {
                        if (item1.FactorySpecificationComments == null || item1.FactorySpecificationComments.Count == 0)
                        {
                            continue;
                        }

                        foreach (var item2 in item1.FactorySpecificationComments)
                        {
                            int subDays = DateTime.Now.Subtract(item2.UpdatedDate.ConvertStringToDateTime().Value).Days;

                            item2.CanDelete = (userID == item2.UpdatedBy && subDays <= 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public ClientSpecificationStandardFileDTO GetStandardFile(int typeID, int clientID, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            ClientSpecificationStandardFileDTO data = new ClientSpecificationStandardFileDTO();

            try
            {
                if (typeID == 1) // EUROFAR STANDARD
                {
                    using (var context = CreateContext())
                    {
                        data.FriendlyName = "eurofar_standard.docx";
                        data.FileLocation = FrameworkSetting.Setting.MediaFullSizeUrl + "temp/" + data.FriendlyName;
                    }
                }
                else // CLIENT STANDARD
                {
                    using (var context = CreateContext())
                    {
                        data = converter.DB2DTO_ClientSpecificationStandardFile(context.ClientSpecificationMng_StandardFile_View.FirstOrDefault(o => o.ClientID == clientID));
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
