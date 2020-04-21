using Library.DTO;
using Module.AdditionalConditionMng.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AdditionalConditionMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private AdditionalConditionEntities Createcontext()
        {
            return new AdditionalConditionEntities(Library.Helper.CreateEntityConnectionString("DAL.AdditionalConditionModel"));
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification() { Type = NotificationType.Success };

            SearchFormData data = new SearchFormData() { AdditionalConditionSearch = new List<AdditionalConditionSearch>() };

            try
            {
                using (var context = Createcontext())
                {
                    int? typeAC = null;
                    int? updateBy = null;
                    string additionalConditionNM = null;
                    string remark = null;
                    if (filters.ContainsKey("TypeAC") && filters["TypeAC"] != null)
                    {
                        typeAC = Convert.ToInt32(filters["TypeAC"]);
                    }
                    if (filters.ContainsKey("AdditionalConditionNM") && !string.IsNullOrEmpty(filters["AdditionalConditionNM"].ToString()))
                    {
                        additionalConditionNM = filters["AdditionalConditionNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("Remark") && !string.IsNullOrEmpty(filters["Remark"].ToString()))
                    {
                        remark = filters["Remark"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("UpdateBy") && filters["UpdateBy"] != null)
                    {
                        updateBy = Convert.ToInt32(filters["UpdateBy"]);
                    }
                    totalRows = context.AdditionalConditionMng_function_AdditionalConditionSearchResult(typeAC, additionalConditionNM, remark, updateBy, orderBy, orderDirection).Count();
                    data.AdditionalConditionSearch = this.converter.BD2DTO_AdditionalConditionSearchResult(context.AdditionalConditionMng_function_AdditionalConditionSearchResult(typeAC, additionalConditionNM, remark, updateBy, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
                AdditionalConditionDTO = new AdditionalConditionDTO()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = Createcontext())
                    {
                        var item = context.AdditionalConditionMng_AdditionalCondition_View.FirstOrDefault(o => o.AdditionalConditionID == id);
                        if (item == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        }
                        else
                        {
                            data.AdditionalConditionDTO = this.converter.DB2DTO_AdditionalConditionDTO(item);
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
                using (var Context = Createcontext())
                {
                    AdditionalCondition additionalCondition = Context.AdditionalCondition.FirstOrDefault(o => o.AdditionalConditionID == id);

                    if (additionalCondition == null)
                    {
                        notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }

                    Context.AdditionalCondition.Remove(additionalCondition);
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
            DTO.AdditionalConditionDTO additionalConditionDTO = ((JObject)dtoItem).ToObject<DTO.AdditionalConditionDTO>();

            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using (var context = Createcontext())
                {
                    AdditionalCondition additionalCondition = new AdditionalCondition();

                    if (id == 0)
                    {
                        context.AdditionalCondition.Add(additionalCondition);
                    }

                    if (id > 0)
                    {
                        additionalCondition = context.AdditionalCondition.FirstOrDefault(o => o.AdditionalConditionID == id);

                        if (additionalCondition == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                            return false;
                        }
                    }

                    this.converter.DTO2DB_AdditionalCondition(additionalConditionDTO, ref additionalCondition);
                    additionalCondition.UpdateDate = DateTime.Now;
                    additionalCondition.UpdateBy = userId;
                    context.SaveChanges();
                    dtoItem = this.GetData(additionalCondition.AdditionalConditionID, out notification);
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
                using (AdditionalConditionEntities context = Createcontext())
                {
                    data.AdditionalConditionTypeDTO = converter.DB2DTO_Support_AdditionalConditionTypeDTO(context.Support_AdditionalCondition_View.ToList());
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
